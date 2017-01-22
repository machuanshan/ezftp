using System;
using System.IO;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("RMD")]
  [FtpCommand("XRMD")]
  class RemoveDirCommand : FtpCommand
  {
    public RemoveDirCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
      string dir = GetPath(cmdValue);

      if (!Directory.Exists(dir))
      {
        SendResponse(550, "Directory does not exist");
        return;        
      }

      try
      {
        Directory.Delete(dir);
        SendResponse(250, "Directory removed");
      }
      catch (Exception e)
      {
        SendResponse(550, $"Couldn't remove directory ({dir})");
      }
    }
  }
}
