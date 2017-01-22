using System;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("PASS")]
	class PasswordCommand : FtpCommand
	{
    public PasswordCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			if (Connection.Login(cmdValue))
			{
				SendResponse(220, "Password ok, FTP server ready");
			}
			else
			{
				SendResponse(530, "Username or password incorrect");
			}
		}
	}
}
