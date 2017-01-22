using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzFtp.Common.Logging
{
  public class LogEntry
  {
    public LogEntry(string msg) : this(msg, Severity.Info)
    { }

    public LogEntry(string msg, Severity level)
    {
      Message = msg;
      Level = level;
    }

    public string Message { get; }
    public Severity Level { get; }
  }
}
