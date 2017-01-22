using System.Net;

namespace EzFtp.Droid.FtpImplementation
{
  public static class FtpSettings
  {
    public const int Port = 9999;
    public const int PassivePort = 9998;
    public const int BufferSize = 65536;

    public static IPAddress LocalAddress { get; set; }
  }
}