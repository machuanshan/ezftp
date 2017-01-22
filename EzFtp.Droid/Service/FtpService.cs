using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using EzFtp.Droid.FtpImplementation;
using static EzFtp.Droid.Constants;

namespace EzFtp.Droid
{
  [Service(Name = "com.clickstone.ezftp.ftpservice")]
  public class FtpService : Service
  {
    public FtpServer FtpServer { get; private set; }

    public override void OnCreate()
    {
      base.OnCreate();
      FtpServer = new FtpServer();
    }

    [return: GeneratedEnum]
    public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    {
      return StartCommandResult.NotSticky;
    }

    public override IBinder OnBind(Intent intent)
    {
      return new FtpServiceBinder(this);
    }

    public override void OnDestroy()
    {
      base.OnDestroy();
      FtpServer.Stop();
    }
  }
}