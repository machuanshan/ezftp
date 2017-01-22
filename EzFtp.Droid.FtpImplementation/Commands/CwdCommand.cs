using System;
using System.IO;
using System.Linq;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("CWD")]
	class CwdCommand : FtpCommand
	{
    public CwdCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
      var invalidChars = Path.GetInvalidPathChars();

      if (invalidChars.Any(c => cmdValue.Contains(c)))
			{
				SendResponse(550, "Not a valid directory string.");
        return;
      }

			string dir = GetPath(cmdValue);

			if (!Directory.Exists(dir))
			{
				SendResponse(550, "Not a valid directory.");
        return;
			}
			
			Connection.CurrentDirectory = dir;
			SendResponse(250, string.Format("CWD Successful ({0})", Connection.CurrentDirectory.Replace("\\", "/")));
		}
	}
}
