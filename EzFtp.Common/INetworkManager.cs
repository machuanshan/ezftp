using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EzFtp.Common
{
  public interface INetworkManager
  {
    bool IsWifiConnected { get; }
  }
}
