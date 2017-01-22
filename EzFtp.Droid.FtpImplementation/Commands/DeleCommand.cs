using System;
using System.IO;

namespace EzFtp.Droid.FtpImplementation
{
	/// <summary>
	/// Delete command handler
	/// </summary>
  [FtpCommand("DELE")]
  class DeleCommand : FtpCommand
	{
    public DeleCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
      try
      {
        string file = GetPath(cmdValue);

        if (!File.Exists(file))
        {
          SendResponse(550, "File does not exist.");
          return;
        }

        File.Delete(file);
        SendResponse(250, "File deleted successfully");
      }
      catch (Exception e)
			{
				SendResponse(550, "Couldn't delete file.");
			}
		}
	}
}
