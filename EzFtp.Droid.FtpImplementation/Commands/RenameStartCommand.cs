using System;
using System.IO;

namespace EzFtp.Droid.FtpImplementation
{
	/// <summary>
	/// Starts a rename file operation
	/// </summary>
  [FtpCommand("RNFR")]
	class RenameStartCommand : FtpCommand
	{
    public RenameStartCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			string file = GetPath(cmdValue);
			Connection.FileToRename = file;

      try
      {
        var info = new FileInfo(file);
        Connection.RenameDirectory = (info.Attributes & FileAttributes.Directory) > 0;
        SendResponse(350, $"Rename file started ({file})");
      }
      catch(Exception e)
			{
				SendResponse(550, $"File does not exist ({file})");
			}						
		} 
	}
}
