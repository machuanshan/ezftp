using System;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("LIST")]
  class ListCommand : ListCommandBase
  {
    public ListCommand(FtpConnection conn)
      : base(conn)
    { }

    protected override string BuildReply(string cmdValue, string[] dirs, string[] files)
    {
      return BuildLongReply(dirs, files);
    }
  }
}
