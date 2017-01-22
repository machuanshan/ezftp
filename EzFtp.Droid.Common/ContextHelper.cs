using Android.App;
using Android.Content;
using Android.Net.Wifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzFtp.Droid.Common
{
  public static class ContextHelper
  {
    public static T GetService<T>(this Context ctx, string serviceName) where T : Java.Lang.Object
    {
      return (T)ctx.GetSystemService(serviceName);
    }

    public static WifiManager GetWifiManager(this Context ctx)
    {
      return ctx.GetService<WifiManager>(Context.WifiService);
    }

    public static NotificationManager GetNotificationManager(this Context ctx)
    {
      return ctx.GetService<NotificationManager>(Context.NotificationService);
    }
  }
}
