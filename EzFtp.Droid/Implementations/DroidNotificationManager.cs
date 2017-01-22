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
using EzFtp.Common;
using static EzFtp.Droid.Constants;
using EzFtp.Droid.Common;

namespace EzFtp.Droid.Implementations
{
  class DroidNotificationManager : INotificationManager
  {
    private Context _ctx;

    public DroidNotificationManager(Context ctx)
    {
      _ctx = ctx;
    }

    public void SendFtpRunningNotification(string ftpAddr)
    {
      var intent = new Intent(_ctx, typeof(MainActivity));
      var pendingIntent = PendingIntent.GetActivity(_ctx, 0, intent, PendingIntentFlags.OneShot);
      var svcIntent = new Intent(StopServiceAction);
      svcIntent.PutExtra(Command, StopServiceCommand);
      var svcPendingIntent = PendingIntent.GetBroadcast(_ctx, 0, svcIntent, PendingIntentFlags.OneShot);

      var n = new Notification.Builder(_ctx)
        .SetContentIntent(pendingIntent)
        .SetSmallIcon(Resource.Drawable.app)
        .SetContentTitle(NetResource.ServerRunningTitle)
        .SetContentText(ftpAddr)
        .SetOngoing(true)
        .SetAutoCancel(true)
        .SetDefaults(NotificationDefaults.Sound)
        .AddAction(new Notification.Action(Resource.Drawable.stopftp, NetResource.StopFtpServer, svcPendingIntent))
        .Build();

      _ctx.GetNotificationManager()
        .Notify(FTPRunningNotificationId, n);
    }
  }
}