using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EzFtp.Common.Logging;
using System.IO;

namespace EzFtp.Droid.FtpImplementation
{
  /// <summary>
	/// Base class for all ftp command handlers.
	/// </summary>
	abstract class FtpCommand
  {
    protected FtpCommand(FtpConnection conn)
    {
      Connection = conn;
    }

    public FtpConnection Connection { get; }

    public abstract void Process(string cmdName, string cmdValue);

    protected void SendResponse(int returnCode, string statusMsg)
    {
      string fullMsg = $"{returnCode} {statusMsg}";
      Logger.Info($"{Connection.Id}: {fullMsg}");
      Connection.TcpClient.Send($"{fullMsg}\r\n", Connection.Encoding);
    }

    protected string GetPath(string path = "")
    {
      if (path.Length == 0)
      {
        return Connection.CurrentDirectory;
      }

      return Path.Combine(Connection.CurrentDirectory, path);
    }
  }
}