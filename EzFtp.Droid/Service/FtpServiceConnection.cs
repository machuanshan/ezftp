using Android.Content;
using Android.OS;
using System;

namespace EzFtp.Droid
{
  class FtpServiceConnection : Java.Lang.Object, IServiceConnection
  {
    public event EventHandler ServiceConnected;
    public event EventHandler ServiceDisconnected;

    public FtpServiceBinder Binder { get; private set; }
    public bool IsConnected { get; private set; }

    public void OnServiceConnected(ComponentName name, IBinder service)
    {
      Binder = service as FtpServiceBinder;
      IsConnected = Binder != null;
      ServiceConnected?.Invoke(this, EventArgs.Empty);
    }

    public void OnServiceDisconnected(ComponentName name)
    {
      Binder = null;
      IsConnected = false;
      ServiceDisconnected?.Invoke(this, EventArgs.Empty);
    }
  }
}