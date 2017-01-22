using EzFtp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EzFtp
{
  public partial class App : Application
  {
    private IFtpDroidService _fptDroidServer;
    private INotificationManager _nm;

    public App()
    {
      InitializeComponent();

      _fptDroidServer = Locator.Resolve<IFtpDroidService>();
      _nm = Locator.Resolve<INotificationManager>();

      MainPage = new NavigationPage(new MainPage());
    }

    protected override void OnStart()
    {
      _fptDroidServer.Start();
      _fptDroidServer.Attach();
    }

    protected override void OnSleep()
    {
      _fptDroidServer.Detach();

      if(_fptDroidServer.Server.Started)
      {
        _nm.SendFtpRunningNotification(_fptDroidServer.Server.FtpAddress);
      }
    }

    protected override void OnResume()
    {
      _fptDroidServer.Attach();
    }
  }
}
