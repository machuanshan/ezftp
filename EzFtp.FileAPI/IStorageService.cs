using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzFtp.FileAPI
{
  public interface IStorageService
  {
    Phone Root { get; }
    DiskItem[] GetChildren(DiskItem diskItem);
  }
}
