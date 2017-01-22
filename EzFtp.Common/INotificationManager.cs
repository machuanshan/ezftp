using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzFtp.Common
{
  public interface INotificationManager
  {
    void SendFtpRunningNotification(string ftpAddr);
  }
}
