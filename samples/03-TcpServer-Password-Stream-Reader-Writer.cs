using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class Program {
	static void Main(string[] arguments) {
		var serverEndpoint = new IPEndPoint(IPAddress.Loopback, 14411);
		var tcpListener = new TcpListener(serverEndpoint);
		tcpListener.Start();
    
		while (true) {
			var tcpClient = tcpListener.AcceptTcpClient();
			var writer = new StreamWriter(tcpClient.GetStream());
			writer.AutoFlush = true;
			writer.WriteLine("What's the password?");
			var response = new StreamReader(tcpClient.GetStream()).ReadLine();
			if (!response.Equals("123456")) {
				writer.WriteLine("Boo!");
				tcpClient.Close();
			} else {
				writer.WriteLine("Nice!");
				tcpClient.Close();
			}
		}

		return;
	}
}