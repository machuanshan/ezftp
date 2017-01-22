using System;
using Android.Content;
using EzFtp.Common;
using EzFtp.Droid.FtpImplementation;
using EzFtp.Droid.Common;
using System.Net;

namespace EzFtp.Droid.Implementations
{
  class FtpDroidService : IFtpDroidService
  {
    private Context _ctx;
    private FtpServiceConnection _svcConn;

    public FtpDroidService(Context ctx)
    {
      _ctx = ctx;
    }

    public IFtpServer Server { get; private set; }
    public event EventHandler Attached;
    public event EventHandler Detached;

    public void Attach()
    {
      _svcConn = _svcConn ?? new FtpServiceConnection();

      if (_svcConn.IsConnected)
      {
        return;
      }

      var svcIntent = new Intent(_ctx, typeof(FtpService));
      var isBound = _ctx.BindService(svcIntent, _svcConn, Bind.AutoCreate);
      _svcConn.ServiceConnected += (s, e) =>
      {
        Server = _svcConn.Binder.FtpService.FtpServer;
        Attached?.Invoke(this, EventArgs.Empty);
      };
      _svcConn.ServiceDisconnected += (s, e) =>
      {
        Server = null;
        Detached?.Invoke(this, EventArgs.Empty);
      };
    }

    public void Detach()
    {
      if (_svcConn?.IsConnected == true)
      {
        _ctx.UnbindService(_svcConn);
      }
    }

    public void Start()
    {
      var intIp = _ctx.GetWifiManager()?.ConnectionInfo?.IpAddress;
      if (intIp.HasValue)
      {
        FtpSettings.LocalAddress = new IPAddress(BitConverter.GetBytes(intIp.Value));
      }

      var svcIntent = new Intent(_ctx, typeof(FtpService));
      var svcName = _ctx.StartService(svcIntent);
    }

    public void Stop()
    {
      if (Server.Started)
      {
        _ctx.UnbindService(_svcConn);
        _ctx.StopService(new Intent(_ctx, typeof(FtpService)));
      }
    }
  }
}