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
using System.Net.Sockets;

namespace EzFtp.Droid.FtpImplementation
{
  /// <summary>
	/// Processes incoming messages and passes the data on to the relevant handler class.
	/// </summary>
	class FtpConnection
  {
    public FtpConnection(int id, TcpClient socket)
    {
      Id = id;
      TcpClient = socket;
    }

    public int Id { get; }

    public TcpClient TcpClient { get; }

    public string User { get; private set; }

    public string CurrentDirectory { get; set; } = "/";

    public Encoding Encoding { get; set; } = Encoding.ASCII;
    /// <summary>
    /// Socket address from PORT command.
    /// </summary>
    public string PortCommandSocketAddress { get; set; } = string.Empty;

    /// <summary>
    /// Port from PORT command.
    /// See FtpReplySocket class.
    /// </summary>
    public int PortCommandSocketPort { get; set; } = 20;

    /// <summary>
    /// Whether the connection is in binary or ASCII transfer mode.
    /// </summary>
    public bool BinaryMode { get; set; } = false;

    /// <summary>
    /// If this is non-null the last command was a PASV and should therefore use this socket.
    /// If this is null the last command was a PORT command and should therefore use that mechanism instead.
    /// </summary>
    public TcpClient PasvTcpClient { get; set; }

    /// <summary>
    /// Rename takes place with 2 commands - we store the old name here
    /// </summary>
    public string FileToRename { get; set; }

    /// <summary>
    /// This is true if the file to rename is a directory
    /// </summary>
    public bool RenameDirectory { get; set; }

    /// <summary>
    /// No user authentication for now.
    /// </summary>
    public bool Login(string user, string password = "")
    {
      User = user;
      return true;
    }
  }
}