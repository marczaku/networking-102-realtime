using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class Program {
    static void Main(string[] arguments) {
        var endpoint = new IPEndPoint(
            // IP-Address: Used with IP-Protocol to find the right computer
            IPAddress.Loopback, // 127.0.0.1 
            // Port: Used with TCP / UDP Protocol to find the right program on a computer
            14411
        );
        var tcpListener = new TcpListener(endpoint);
        tcpListener.Start();
        
        while (true) {
            var tcpClient = tcpListener.AcceptTcpClient();
            // We CAN (but don't have to) Read from the Client
            byte[] buffer = new byte[100];
            tcpClient.GetStream().Read(buffer, 0, 100);
            Console.WriteLine("Client said: "+Encoding.ASCII.GetString(buffer));
            // We CAN (but don't have to) Write To the Client
            var responseBuffer = Encoding.ASCII.GetBytes("Hello World.");
            tcpClient.GetStream().Write(responseBuffer, 0, responseBuffer.Length);
            // You could do more stuff with this client. Or just close it already:
            tcpClient.Close();
        }
    }
}