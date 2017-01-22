using System;
using System.IO;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("MKD")]
  [FtpCommand("XMKD")]
  class MakeDirCommand : FtpCommand
	{
    public MakeDirCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			string dir = GetPath(cmdValue);

      try
      {
        Directory.CreateDirectory(dir);
        SendResponse(257, dir);
      }
      catch (Exception e)
			{
				SendResponse(550, $"Couldn't create directory. ({dir})");
			}
		}

	}
}
