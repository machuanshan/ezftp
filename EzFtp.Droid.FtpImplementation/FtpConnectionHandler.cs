using EzFtp.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;

namespace EzFtp.Droid.FtpImplementation
{
  class FtpConnectionHandler
  {
    private static Dictionary<string, Type> _commandTypes = new Dictionary<string, Type>();

    static FtpConnectionHandler()
    {
      var currentAsm = Assembly.GetExecutingAssembly();
      var types = currentAsm.GetTypes()
        .Where(t => typeof(FtpCommand).IsAssignableFrom(t));

      foreach (var cmdType in types)
      {
        var attrs = cmdType.GetCustomAttributes<FtpCommandAttribute>();
        foreach (var attr in attrs)
        {
          _commandTypes[attr.CommandName] = cmdType;
        }
      }
    }

    private TcpClient _tcpClient;
    private Thread _thread;
    private FtpConnection _ftpConn;
    private Dictionary<string, FtpCommand> _cmds;
    private FtpCommand _unknownCmd;

    public event Action<FtpConnectionHandler> Closed;
    public int Id { get; }

    public FtpConnectionHandler(int id, TcpClient tcpClient)
    {
      Id = id;
      _tcpClient = tcpClient;
      _ftpConn = new FtpConnection(Id, _tcpClient);
      _unknownCmd = new UnknownCommand(_ftpConn);
      _cmds = _commandTypes.ToDictionary(
        kv => kv.Key,
        kv => (FtpCommand)Activator.CreateInstance(kv.Value, _ftpConn));
    }

    public void Start()
    {
      _thread = new Thread(HandleClient);
      _thread.Start();
    }

    public void Stop()
    {
      _tcpClient.Close();
      _thread.Join();
    }

    private void HandleClient()
    {
      try
      {
        byte[] data = new byte[FtpSettings.BufferSize];
        var stream = _tcpClient.GetStream();
        int received = stream.Read(data, 0, FtpSettings.BufferSize);

        while (received > 0)
        {
          ProcessCommand(data);
          received = stream.Read(data, 0, FtpSettings.BufferSize);
        }

        Logger.Info($"{Id}: Connection closed");
      }
      catch (Exception e)
      {
        Logger.Error($"{Id}: Connection aborted, {e.Message}");
      }

      _tcpClient.Close();
      Closed?.Invoke(this);
    }

    private void ProcessCommand(byte[] data)
    {
      string msg = _ftpConn.Encoding.GetString(data);
      msg = msg.Substring(0, msg.IndexOf('\r'));

      Logger.Info($"{Id}: {msg}");

      string cmdName;
      string cmdValue;
      int spaceIndex = msg.IndexOf(' ');

      if (spaceIndex < 0)
      {
        cmdName = msg.ToUpper();
        cmdValue = string.Empty;
      }
      else
      {
        cmdName = msg.Substring(0, spaceIndex).ToUpper();
        cmdValue = msg.Substring(spaceIndex + 1);
      }

      var cmd = _cmds.ContainsKey(cmdName) ? _cmds[cmdName] : _unknownCmd;
      cmd.Process(cmdName, cmdValue);
    }
  }
}