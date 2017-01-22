using System;
using System.IO;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("STOR")]
	class StoreCommand : FtpCommand
	{
    public StoreCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			string fileName = GetPath(cmdValue);
			
			if (File.Exists(fileName))
			{
				SendResponse(553, "File already exists.");
        return;
			}
      
      try
      {
        using (var file = File.OpenWrite(fileName))
        {
          var socketReply = new FtpReplySocket(Connection);
          byte[] data = new byte[FtpSettings.BufferSize];
          SendResponse(150, "Opening connection for data transfer.");
          int received = socketReply.Receive(data);

          while (received > 0)
          {
            file.Write(data, 0, received);
            received = socketReply.Receive(data);
          }

          socketReply.Close();
          SendResponse(226, "Uploaded file successfully.");
        }
      }
			catch(Exception e)
			{
				SendResponse(425, "Error in establishing data connection.");
			}      
		}
	}
}
