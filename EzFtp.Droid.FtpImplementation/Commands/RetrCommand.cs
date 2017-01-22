using System;
using System.IO;

namespace EzFtp.Droid.FtpImplementation
{
	/// <summary>
	/// Implements the RETR command
	/// </summary>
  [FtpCommand("RETR")]
	class RetrCommand : FtpCommand
	{
    public RetrCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			string fileName = GetPath(cmdValue);
			
			if (!File.Exists(fileName))
			{
				SendResponse(550, "File doesn't exist");
        return;
      }
      
      try
      {
        FtpReplySocket replySocket = new FtpReplySocket(Connection);
        SendResponse(150, "Starting data transfer, please wait...");

        using (var file = File.OpenRead(fileName))
        {
          byte[] buffer = new byte[FtpSettings.BufferSize];
          int read = file.Read(buffer, 0, FtpSettings.BufferSize);

          while (read > 0)
          {
            replySocket.Send(buffer, read);
            read = file.Read(buffer, 0, FtpSettings.BufferSize);
          }
        }

        replySocket.Close();
        SendResponse(226, "File download succeeded.");
      }
      catch(IOException ioEx)
      {
        SendResponse(550, "Couldn't open file");
      }
      catch (Exception e)
			{
				SendResponse(550, "Unable to establish data connection");
			}			
		}
	}
}
