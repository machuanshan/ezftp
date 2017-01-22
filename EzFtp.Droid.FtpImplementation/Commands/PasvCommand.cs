using System;
using System.Net;
using System.Net.Sockets;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("PASV")]
  class PasvCommand : FtpCommand
  {
    public PasvCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
      if (Connection.PasvTcpClient == null)
      {
        try
        {
          var listener = new TcpListener(FtpSettings.LocalAddress, FtpSettings.PassivePort);          
          SendPasvReply();
          listener.Start();
          Connection.PasvTcpClient = listener.AcceptTcpClient();
          listener.Stop();
        }
        catch(Exception e)
        {
          SendResponse(550, $"Couldn't start listener on port {FtpSettings.PassivePort}, {e.Message}");
        }
      }
      else
      {
        SendPasvReply();
      }
    }

    private void SendPasvReply()
    {
      //227 = h1,h2,h3,h4,p1,p2
      //where the server's IP address is h1.h2.h3.h4 
      //and the TCP port number is p1*256+p2.
      string ipAddr = FtpSettings.LocalAddress.ToString();
      ipAddr = ipAddr.Replace('.', ',');
      ipAddr += ',';
      ipAddr += FtpSettings.PassivePort / 256;
      ipAddr += ',';
      ipAddr += FtpSettings.PassivePort % 256;
      SendResponse(227, $"={ipAddr}");
    }
  }
}
