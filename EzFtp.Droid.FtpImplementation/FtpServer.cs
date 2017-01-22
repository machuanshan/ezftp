using EzFtp.Common;
using EzFtp.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace EzFtp.Droid.FtpImplementation
{
  public class FtpServer : IFtpServer
  {
    private TcpListener _tcpListener;

    private Thread _workerThread;
    private int _id;
    private List<FtpConnectionHandler> _connections;

    public bool Started { get; private set; } = false;
    public string FtpAddress
    {
      get { return $"{FtpSettings.LocalAddress}:{FtpSettings.Port}"; }
    }

    public FtpServer()
    {
      _connections = new List<FtpConnectionHandler>();
    }

    /// <summary>
    /// Cannot binding to port less than 1024 without admin previlege.        
    /// </summary>
    public void Start()
    {
      if (Started) return;

      _workerThread = new Thread(ListenConnection);
      _workerThread.Start();
      Started = true;
    }

    public void Stop()
    {
      if (!Started) return;
      Started = false;

      try
      {
        for (int i = _connections.Count - 1; i >= 0; i--)
        {
          _connections[i].Stop();
        }

        _tcpListener.Stop();
        _workerThread.Join();
      }
      catch
      {
        Logger.Error("FTP server has been stopped.");
      }
    }

    private void ListenConnection()
    {
      try
      {
        _tcpListener = new TcpListener(FtpSettings.LocalAddress, FtpSettings.Port);
        _tcpListener.Start();
        Logger.Info("0: FTP server started");

        while (true)
        {
          try
          {
            var socket = _tcpListener.AcceptTcpClient();
            socket.NoDelay = false;
            Logger.Info($"{++_id}: New connection");
            socket.Send($"220 FTP Server Ready{Environment.NewLine}");

            var handler = new FtpConnectionHandler(_id, socket);
            handler.Closed += h => _connections.Remove(h);
            handler.Start();
            _connections.Add(handler);
          }
          catch (Exception se)
          {
            Logger.Error($"0: Error in accept FTP client: {se.Message}");
            break;
          }
        }
      }
      catch (Exception e)
      {
        Logger.Error($"Error in starting FTP server: {e.Message}");
      }
    }
  }
}
