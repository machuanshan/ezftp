using System;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("ALLO")]
	class AlloCommand : FtpCommand
	{
    public AlloCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			SendResponse(202, "Allo processed successfully (depreciated).");
		}
	}
}
