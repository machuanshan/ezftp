using Android.App;
using Android.Content;
using EzFtp.Droid.Common;
using static EzFtp.Droid.Constants;
using static Android.Content.Context;
using System.Diagnostics;

namespace EzFtp.Droid
{
  [BroadcastReceiver(Enabled = true)]
  [IntentFilter(new[] { StopServiceAction })]
  class NotificationReceiver : BroadcastReceiver
  {
    public override void OnReceive(Context context, Intent intent)
    {
      if (intent.GetStringExtra(Command) == StopServiceCommand)
      {
        var intentStopSvc = new Intent(context, typeof(FtpService));
        var stopped = context.StopService(intentStopSvc);

        context.GetNotificationManager()
          .Cancel(FTPRunningNotificationId);
      }
    }
  }
}