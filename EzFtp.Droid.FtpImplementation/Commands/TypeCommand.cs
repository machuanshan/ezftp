using System;

namespace EzFtp.Droid.FtpImplementation
{
	/// <summary>
	/// Implements the 'TYPE' command
	/// </summary>
  [FtpCommand("TYPE")]
	class TypeCommand : FtpCommand
	{
    public TypeCommand(FtpConnection conn)
      : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
			cmdValue = cmdValue.ToUpper();

			if (cmdValue == "A")
			{
				Connection.BinaryMode = false;
				SendResponse(200, "ASCII transfer mode active.");
			}
			else if (cmdValue == "I")
			{
				Connection.BinaryMode = true;
				SendResponse(200, "Binary transfer mode active.");
			}
			else
			{
				SendResponse(550, $"Error - unknown binary mode \"{cmdValue}\"");
			}
		}
	}
}
