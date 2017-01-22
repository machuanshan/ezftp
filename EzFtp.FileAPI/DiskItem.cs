using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzFtp.FileAPI
{
  public abstract class DiskItem
  {
    public string RootPath { get; set; }
    public string RelativePath { get; set; }
    public string Icon { get; set; }
    public virtual string Name { get; set; }

    public string AbsolutePath => RootPath + RelativePath;

    public override bool Equals(object obj)
    {
      if (obj == null) return false;
      if (obj.GetType() != GetType()) return false;

      return ((DiskItem)obj).RelativePath == RelativePath;
    }

    public override int GetHashCode()
    {
      return RelativePath.GetHashCode();
    }
  }
}
