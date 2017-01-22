using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzFtp.Common
{
  public interface IFtpDroidService
  {
    void Start();
    void Attach();
    void Detach();
    void Stop();

    event EventHandler Attached;
    event EventHandler Detached;

    IFtpServer Server { get; }
  }
}
