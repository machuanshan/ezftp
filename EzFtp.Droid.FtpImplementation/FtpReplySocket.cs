using System;
using System.Net.Sockets;
using System.Text;

namespace EzFtp.Droid.FtpImplementation
{
  class FtpReplySocket
  {
    private TcpClient _socket;
    private FtpConnection _conn;

    public FtpReplySocket(FtpConnection conn)
    {
      _conn = conn;
      _socket = OpenSocket(conn);
    }

    public void Close()
    {
      _socket.Close();
    }

    public void Send(byte[] data, int size)
    {
      _socket.Send(data, 0, size);
    }

    public void Send(string msg)
    {
      _socket.Send(msg, _conn.Encoding);
    }

    public int Receive(byte[] data)
    {
      return _socket.GetStream().Read(data, 0, data.Length);
    }

    private TcpClient OpenSocket(FtpConnection connectionObject)
    {
      TcpClient socketPasv = connectionObject.PasvTcpClient;

      if (socketPasv != null)
      {
        connectionObject.PasvTcpClient = null;
        return socketPasv;
      }

      return new TcpClient(
        connectionObject.PortCommandSocketAddress,
        connectionObject.PortCommandSocketPort);
    }
  }
}