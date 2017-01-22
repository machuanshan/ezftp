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
using EzFtp.Droid.Common;
using System.Net;

namespace EzFtp.Droid.Implementations
{
  public class NetworkManager : INetworkManager
  {
    private Context _ctx;

    public NetworkManager(Context ctx)
    {
      _ctx = ctx;
    }

    public bool IsWifiConnected
    {
      get
      {
        var ipAddress = _ctx.GetWifiManager().ConnectionInfo?.IpAddress;        
        return ipAddress != null && ipAddress.Value != 0;
      }
    }
  }
}