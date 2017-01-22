using EzFtp.FileAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EzFtp
{
  public partial class MainPage : ContentPage
  {
    private MainPageViewModel _viewModel;

    public MainPage()
    {
      InitializeComponent();
      BindingContext = _viewModel = new MainPageViewModel();
      _viewModel.View = this;
      _viewModel.Initialize();      
    }

    protected override bool OnBackButtonPressed()
    {
      if (_viewModel.GoBack())
      {
        return true;
      }

      return base.OnBackButtonPressed();
    }

    private void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
      _viewModel.OpenDiskItem(e.Item as DiskItem);
    }

    public void DisplayCurrentPath()
    {
      _pathPanel.Children.Clear();
      foreach (RelativeDirectory diskItem in _viewModel.DirectoryStack)
      {
        var label = new Label();
        label.Text = diskItem.LocalizedName + " >";
        label.VerticalOptions = LayoutOptions.Center;
        var tgr = new TapGestureRecognizer();
        tgr.Tapped += (s, e) =>
        {
          while (diskItem != _viewModel.DirectoryStack.Pop()) ;
          _viewModel.OpenDiskItem(diskItem);
        };

        label.GestureRecognizers.Add(tgr);        
        _pathPanel.Children.Insert(0, label);
      }

      var lastLabel = _pathPanel.Children.Last() as Label;
      lastLabel.Text = lastLabel.Text.Substring(0, lastLabel.Text.Length - 1);
      lastLabel.GestureRecognizers.Clear();      
    }    
  }
}
