using System;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("QUIT")]
	class QuitCommand : FtpCommand
	{
    public QuitCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			SendResponse(220, "Goodbye");
		}
	}
}
