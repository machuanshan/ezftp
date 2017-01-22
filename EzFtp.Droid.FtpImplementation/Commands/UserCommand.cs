using System;

namespace EzFtp.Droid.FtpImplementation
{
  [FtpCommand("USER")]
	class UserCommand : FtpCommand
	{
    public UserCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
      if (Connection.Login(cmdValue))
      {
        SendResponse(220, "Password ok, FTP server ready");
      }
      else
      {
        SendResponse(331, $"User {cmdValue} logged in, needs password");
      }
    }
	}
}
