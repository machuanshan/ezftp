namespace EzFtp.Droid.FtpImplementation
{
  class UnknownCommand : FtpCommand
  {
    public UnknownCommand(FtpConnection conn) : base(conn)
    { }

    public override void Process(string cmdName, string cmdValue)
    {
      SendResponse(550, "Unknown command");
    }
  }
}