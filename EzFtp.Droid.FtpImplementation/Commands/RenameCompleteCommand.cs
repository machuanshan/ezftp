using System;
using System.IO;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("RNTO")]
	class RenameCompleteCommand : FtpCommand
	{
    public RenameCompleteCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			if (Connection.FileToRename.Length == 0)
			{
				SendResponse(503, "RNTO must be preceded by a RNFR.");
        return;
			}
      
      try
      {
        string newFileName = GetPath(cmdValue);
        string oldFileName = Connection.FileToRename;
        Connection.FileToRename = "";

        if (Connection.RenameDirectory)
        {
          if (Directory.Exists(newFileName))
          {
            SendResponse(553, $"Directory already exists ({newFileName}).");
          }
          else
          {
            Directory.Move(oldFileName, newFileName);
            SendResponse(250, "Renamed directory successfully.");
          }
        }
        else
        {
          if (File.Exists(newFileName))
          {
            SendResponse(553, $"File already exists ({newFileName})."); 
          }
          else
          {
            File.Move(oldFileName, newFileName);
            SendResponse(250, "Renamed file successfully.");
          }
        }
      }
      catch(Exception e)
			{
				SendResponse(553, "Move failed");
			}
		}
	}
}
