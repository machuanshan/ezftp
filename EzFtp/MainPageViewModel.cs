using EzFtp.FileAPI;
using EzFtp.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using EzFtp.Properties;

namespace EzFtp
{
  public class MainPageViewModel : ViewModelBase
  {
    private IStorageService _ss;
    private INetworkManager _nm;
    private IFtpDroidService _droidSvc;
    private DiskItem _selectedItem;
    private bool _isStarted;
    private string _ftpAddress;

    public Stack<DiskItem> DirectoryStack { get;  }

    public ObservableCollection<DiskItem> DiskItems { get; private set; }

    public ICommand StartServiceCommand { get; }

    public ICommand StopServiceCommand { get; }

    public DiskItem SelectedItem
    {
      get { return _selectedItem; }
      set
      {
        _selectedItem = value;
      }
    }

    public bool IsStarted
    {
      get { return _isStarted; }
      set
      {
        _isStarted = true;
        OnPropertyChanged();
      }
    }

    public string FtpAddress
    {
      get { return _ftpAddress; }
      set
      {
        _ftpAddress = value;
        OnPropertyChanged();
      }
    }

    public MainPage View { get; set; }

    public MainPageViewModel()
    {
      _ss = DependencyService.Get<IStorageService>(DependencyFetchTarget.GlobalInstance);
      _nm = Locator.Resolve<INetworkManager>();
      _droidSvc = Locator.Resolve<IFtpDroidService>();

      DiskItems = new ObservableCollection<DiskItem>();
      DirectoryStack = new Stack<DiskItem>();
      StartServiceCommand = new Command(OnStartServiceCommand);
      StopServiceCommand = new Command(OnStopServiceCommand);
    }

    public void Initialize()
    {
      OpenDiskItem(_ss.Root);
    }

    private void OnStopServiceCommand()
    {
      _droidSvc.Server.Stop();
      IsStarted = _droidSvc.Server.Started;
      FtpAddress = _droidSvc.Server.FtpAddress;
    }

    private void OnStartServiceCommand()
    {
      if(!_nm.IsWifiConnected)
      {
        View.DisplayAlert(Resources.BtnTextStartFtp, Resources.InvalidConnection, Resources.AlertCancelText);
        return;
      }
      
      _droidSvc.Server.Start();
      IsStarted = _droidSvc.Server.Started;
      FtpAddress = _droidSvc.Server.FtpAddress;
    }

    public bool GoBack()
    {
      if (DirectoryStack.Count <= 1)
      {
        return false;
      }

      DirectoryStack.Pop();
      OpenDiskItem(DirectoryStack.Pop());
      return true;
    }

    public void OpenDiskItem(DiskItem diskItem)
    {
      if (diskItem is RelativeFile)
      {
        return;
      }

      var children = _ss.GetChildren(diskItem ?? _ss.Root);

      DiskItems.Clear();
      DiskItems.AddRange(children);
      DirectoryStack.Push(diskItem);
      View.DisplayCurrentPath();
    }
  }
}
