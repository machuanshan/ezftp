using EzFtp.Common;
using EzFtp.FileAPI;
using EzFtp.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace EzFtp
{
  public class MainPageViewModel : ViewModelBase
  {
    private IStorageService _ss;
    private INetworkManager _nm;
    private IFtpDroidService _droidSvc;
    private DiskItem _selectedItem;
    private RelativeDirectory _currentDirectory;

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

    public RelativeDirectory CurrentDirectory
    {
      get { return _currentDirectory; }
      set
      {
        _currentDirectory = value;
        OnPropertyChanged();
      }
    }

    public bool? IsStarted
    {
      get { return _droidSvc.Server?.Started; }
    }

    public string FtpAddress
    {
      get
      {
        var addr = _droidSvc.Server?.FtpAddress;
        return string.IsNullOrEmpty(addr) ? string.Empty : $"ftp://{addr}";
      }
    }

    public MainPage View { get; set; }

    public MainPageViewModel()
    {
      _ss = DependencyService.Get<IStorageService>(DependencyFetchTarget.GlobalInstance);      
      _nm = Locator.Resolve<INetworkManager>();
      _droidSvc = Locator.Resolve<IFtpDroidService>();
      _droidSvc.Attached += OnServiceAttached;

      DiskItems = new ObservableCollection<DiskItem>();
      DirectoryStack = new Stack<DiskItem>();
      StartServiceCommand = new Command(OnStartServiceCommand);
      StopServiceCommand = new Command(OnStopServiceCommand);
    }

    public void Initialize()
    {
      OpenDiskItem(_ss.Root);      
    }

    private void OnServiceAttached(object sender, EventArgs e)
    {
      OnPropertyChanged("IsStarted");
      OnPropertyChanged("FtpAddress");
    }

    private void OnStopServiceCommand()
    {
      _droidSvc.Server.Stop();
      OnPropertyChanged("IsStarted");
    }

    private void OnStartServiceCommand()
    {
      if(!_nm.IsWifiConnected)
      {
        View.DisplayAlert(Resources.BtnTextStartFtp, Resources.InvalidConnection, Resources.AlertCancelText);
        return;
      }

      CurrentDirectory = DirectoryStack.Peek() as RelativeDirectory;
      _droidSvc.Server.Start();
      OnPropertyChanged("IsStarted");
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
