using System;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("NOOP")]
	class NoopCommand : FtpCommand
	{
    public NoopCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			SendResponse(200, "");
		}
	}
}
