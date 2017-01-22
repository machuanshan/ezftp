using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EzFtp.FileAPI
{
  public class RelativeDirectory : DiskItem
  {
    private string _localizedName;

    public string LocalizedName
    {
      get { return _localizedName ?? Path.GetFileName(RelativePath); }
      set { _localizedName = value; }
    }    
  }
}
