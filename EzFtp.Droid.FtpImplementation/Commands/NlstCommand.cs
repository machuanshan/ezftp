using System;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("NLST")]
	class NlstCommand : ListCommandBase
	{
    public NlstCommand(FtpConnection conn)
      : base(conn)
    { }

		protected override string BuildReply(string cmdValue, string[] dirs, string[] files)
		{
			if (cmdValue == "-L" || cmdValue == "-l")
			{
				return BuildLongReply(dirs, files);
			}
			else
			{
				return BuildShortReply(dirs, files);
			}
		}
	}
}
