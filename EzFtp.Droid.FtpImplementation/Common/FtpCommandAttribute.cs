using System;

namespace EzFtp.Droid.FtpImplementation
{
  [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
  internal sealed class FtpCommandAttribute : Attribute
  {
    public string CommandName { get; }

    public FtpCommandAttribute(string cmdName)
    {
      CommandName = cmdName;
    }
  }
}