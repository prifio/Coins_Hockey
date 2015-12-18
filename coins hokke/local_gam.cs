using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;

namespace coins_hockey
{
    class Server
    {
        TcpListener server;
        TcpClient client;
        byte[] bytes = new byte[1024];
        NetworkStream buf, buf2;
        public void set_server(string ip,int port)
        {
            server = new TcpListener(IPAddress.Parse(ip), port);
            server.Start();
            client = server.AcceptTcpClient();
        }
        public void out_info(string outs)
        {
            buf = client.GetStream();
            buf.Write(System.Text.Encoding.Default.GetBytes(outs), 0, outs.Length);
        }
        public string get_info()
        {
            buf2 = client.GetStream();
            int len2 = buf2.Read(bytes, 0, bytes.Length);
            return System.Text.Encoding.Default.GetString(bytes, 0, len2);
        }
    }
    class Client
    {
        TcpClient client;
        byte[] bytes = new byte[1024];
        NetworkStream buf, buf2;
        public void set_server(string ip, int port)
        {
            client=new TcpClient();
            client.Connect(IPAddress.Parse(ip), port);
        }
        public void out_info(string outs)
        {
            buf = client.GetStream();
            buf.Write(System.Text.Encoding.Default.GetBytes(outs), 0, outs.Length);
        }
        public string get_info()
        {
            buf2 = client.GetStream();
            int len2 = buf2.Read(bytes, 0, bytes.Length);
            return System.Text.Encoding.Default.GetString(bytes, 0, len2);
        }
    }
}
