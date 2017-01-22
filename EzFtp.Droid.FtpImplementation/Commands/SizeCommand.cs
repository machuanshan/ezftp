using System;
using System.IO;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("SIZE")]
  class SizeCommand : FtpCommand
  {
    public SizeCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
      string path = GetPath(cmdValue);

      if (!File.Exists(path))
      {
        SendResponse(550, $"File doesn't exist ({path})");
        return;
      }

      try
      {
        var info = new FileInfo(path);
        SendResponse(220, info.Length.ToString());
      }
      catch(Exception e)
      {
        SendResponse(550, "Error in getting file information");
      }
    }
  }
}
