using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("OPTS")]
  class OptsCommand : FtpCommand
  {
    public OptsCommand(FtpConnection conn) : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
      if(cmdValue.StartsWith("utf8", StringComparison.OrdinalIgnoreCase))
      {
        if(cmdValue.EndsWith("on", StringComparison.OrdinalIgnoreCase))
        {
          Connection.Encoding = Encoding.UTF8;
          SendResponse(200, "UTF8 mode is enabled");
        }
        else if(cmdValue.EndsWith("off", StringComparison.OrdinalIgnoreCase))
        {
          Connection.Encoding = Encoding.ASCII;
          SendResponse(200, "UTF 8 mode is disabled");
        }
      }
      else
      {
        SendResponse(501, "Unknown option");
      }
    }
  }
}