using System;

namespace EzFtp.Droid.FtpImplementation
{
	
  [FtpCommand("XPWD")]
  [FtpCommand("PWD")]
  class PwdCommand : FtpCommand
	{
    public PwdCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			SendResponse(257, $"\"{Connection.CurrentDirectory}\" PWD Successful.");
		}
	}
}
