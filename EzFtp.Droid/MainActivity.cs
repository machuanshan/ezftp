using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using EzFtp.Common;
using EzFtp.Droid.Implementations;

namespace EzFtp.Droid
{
  [Activity(
    Label = "EzFtp", 
    Icon = "@drawable/app", 
    Theme = "@style/MainTheme", 
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    protected override void OnCreate(Bundle bundle)
    {
      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;

      base.OnCreate(bundle);
      
      global::Xamarin.Forms.Forms.Init(this, bundle);

      Locator.Register<INetworkManager>(new NetworkManager(this));
      Locator.Register<IFtpDroidService>(new FtpDroidService(this));
      LoadApplication(new App());
    }
  }
}

