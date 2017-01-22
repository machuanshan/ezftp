using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzFtp.Common.Logging
{
  public static class Logger
  {
    public static event Action<LogEntry> Logging;

    public static void Info(string message)
    {
      Logging?.Invoke(new LogEntry(message));
    }

    public static void Error(string message)
    {
      Logging?.Invoke(new LogEntry(message, Severity.Error));
    }
  }
}
