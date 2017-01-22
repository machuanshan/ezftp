using System;
using System.IO;
using System.Net.Sockets;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("APPE")]
	class AppendCommand : FtpCommand
	{
    public AppendCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			var fileName = GetPath(cmdValue);

      try
      {
        using (var file = File.OpenWrite(fileName))
        {
          file.Seek(0, SeekOrigin.End);

          var socketReply = new FtpReplySocket(Connection);
          var data = new byte[FtpSettings.BufferSize];
          SendResponse(150, "Opening connection for data transfer.");
          var received = socketReply.Receive(data);

          while (received > 0)
          {
            received = socketReply.Receive(data);
            file.Write(data, 0, received);
          }

          file.Close();
          socketReply.Close();
          SendResponse(226, $"Appended file successfully. ({fileName})");
        }
      }
      catch(IOException)
      {
        SendResponse(425, "Couldn't open file");
      }
      catch(SocketException)
      {
        SendResponse(425, "Error in establishing data connection.");
      }
		}
	}
}
