using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace EzFtp.Droid.FtpImplementation
{
	/// <summary>
	/// Base class for list commands
	/// </summary>
	abstract class ListCommandBase : FtpCommand
	{
    public ListCommandBase(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
      try
      {
        SendResponse(150, "Opening data connection for LIST");

        string[] files;
        string[] dirs;
        string curDir = Connection.CurrentDirectory;
        cmdValue = cmdValue.Trim();

        if (cmdValue.Length == 0 || cmdValue[0] == '-')
        {
          files = Directory.GetFiles(curDir);
          dirs = Directory.GetDirectories(curDir);
        }
        else
        {
          files = Directory.GetFiles(curDir, cmdValue);
          dirs = Directory.GetDirectories(curDir, cmdValue);
        }

        string response = BuildReply(cmdValue, dirs, files);
        var socketReply = new FtpReplySocket(Connection);
        socketReply.Send(response);
        socketReply.Close();
        SendResponse(226, "LIST successful.");
      }
			catch(Exception e)
			{
				SendResponse(550, "LIST unable to establish return connection.");
			}			
		}

		protected abstract string BuildReply(string cmdValue, string [] dirs, string[] files);

		protected string BuildShortReply(string[] dirs, string[] files)
		{
      StringBuilder sb = new StringBuilder();

      foreach (var dir in dirs)
      {
        sb.Append(dir);
        sb.Append("\r\n");
      }

      foreach (var file in files)
      {
        sb.Append(file);
        sb.Append("\r\n");
      }

      return sb.ToString();
		}

		protected string BuildLongReply(string[] dirs, string [] files)
		{
			var sb = new StringBuilder();

      foreach (var dir in dirs)
      {
        sb.Append(BuildFileString(dir, true));
      }

      foreach (var file in files)
      {
        sb.Append(BuildFileString(file, false));        
      }

			return sb.ToString();
		}            

    private string BuildFileString(string file, bool isDir)
    {
      var fileInfo = new FileInfo(file);
      bool isReadOnly = (fileInfo.Attributes & FileAttributes.ReadOnly) != 0;
      var wFlag = isReadOnly ? "-" : "w";
      var fileDate = fileInfo.LastWriteTime;      
      var month = GetAbbrMonthName(fileDate);
      var time = fileDate.ToString("hh:mm");
      var fileName = Path.GetFileName(file);

      if(isDir)
      {
        return $"dr{wFlag}xr-xr-x 1 owner group {1,13} {month} {fileDate.Day,2} {time} {fileName}{Environment.NewLine}";
      }

      return $"-r{wFlag}-r--r-- 1 owner group {fileInfo.Length,13} {month} {fileDate.Day,2} {time} {fileName}{Environment.NewLine}";
    }

    private string GetAbbrMonthName(DateTime dateTime)
    {
      return CultureInfo
        .InvariantCulture
        .DateTimeFormat
        .GetAbbreviatedMonthName(dateTime.Month);
    }
  }
}
