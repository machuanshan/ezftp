using System.Text;
using System.Net.Sockets;
using System.IO;

namespace EzFtp.Droid.FtpImplementation
{
  public static class TcpClientExtension
  {
    public static void Send(this TcpClient tc, byte[] msg)
    {
      tc.Send(msg, 0, msg.Length);
    }

    public static void Send(this TcpClient tc, byte[] msg, int start, int length)
    {
      BinaryWriter writer = new BinaryWriter(tc.GetStream());
      writer.Write(msg, start, length);
      writer.Flush();
    }

    public static void Send(this TcpClient tc, string msg)
    {
      Send(tc, msg, Encoding.ASCII);
    }

    public static void Send(this TcpClient tc, string msg, Encoding encoding)
    {
      tc.Send((encoding ?? Encoding.ASCII).GetBytes(msg));
    }
  }
}