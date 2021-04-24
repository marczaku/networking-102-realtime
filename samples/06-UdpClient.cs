using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class Program {
	static void Main(string[] arguments) {
		var serverEndpoint = new IPEndPoint(IPAddress.Loopback, 14411);
		var clientEndpoint = new IPEndPoint(IPAddress.Loopback, 15511);
		// We open the Socket, so we can receive and send Packets
		var client = new UdpClient(clientEndpoint);
		var message = Encoding.ASCII.GetBytes("Hello, Server.");
		// Send a message to the server's end point
		client.Send(message, message.Length, serverEndpoint);
	}
}