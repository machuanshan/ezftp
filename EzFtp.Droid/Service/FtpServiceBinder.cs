using Android.OS;

namespace EzFtp.Droid
{
  class FtpServiceBinder : Binder
  {
    public FtpService FtpService { get; }

    public FtpServiceBinder(FtpService ftpSvc)
    {
      FtpService = ftpSvc;
    }
  }
}