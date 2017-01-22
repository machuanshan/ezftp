using EzFtp.FileAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EzFtp
{
  public class DiskItemDataTemplateSelector : DataTemplateSelector
  {
    public DataTemplate DiskTemplate { get; set; }
    public DataTemplate DirectoryTemplate { get; set; }
    public DataTemplate FileTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
      if(item is Disk)
      {
        return DiskTemplate;
      }

      if (item is RelativeDirectory)
      {
        return DirectoryTemplate;
      }

      if (item is RelativeFile)
      {
        return FileTemplate;
      }

      throw new NotImplementedException();
    }
  }
}
