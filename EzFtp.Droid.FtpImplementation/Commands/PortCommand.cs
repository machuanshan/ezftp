using System;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("PORT")]
	class PortCommand : FtpCommand
	{
    public PortCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			string [] data = cmdValue.Split(new char [] { ',' });

			if (data.Length != 6)
			{
				SendResponse(550, "Error in setting up data connection");
        return;
			}

			int port = int.Parse(data[4]) * 256 + int.Parse(data[5]);

			Connection.PortCommandSocketPort = port;
			Connection.PortCommandSocketAddress = string.Join(".", data, 0, 4);
			
			SendResponse(200, "PORT command succeeded");
		}
	}
}
