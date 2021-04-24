using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class Program {
	static void Main(string[] arguments) {
		var serverEndpoint = new IPEndPoint(IPAddress.Loopback, 14411);
		var clientEndpoint = new IPEndPoint(IPAddress.Loopback,14412);
		// Start a client on our end point
		var tcpClient = new TcpClient(clientEndpoint);
		// And connect to the server's end point
		tcpClient.Connect(serverEndpoint);
		
		// Receive a message from the Server:
		var stream = tcpClient.GetStream();
		byte[] buffer = new byte[100];
		stream.Read(buffer, 0, 100);
		Console.Write("Server said: "+Encoding.ASCII.GetString(buffer));
		
		// Close the Client
		tcpClient.Close();
	}
}