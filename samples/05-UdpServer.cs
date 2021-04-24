using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class Program {
	static void Main(string[] arguments) {
		var serverEndpoint = new IPEndPoint(IPAddress.Loopback, 14411);
		
		// We open the Socket, so we can receive Packets
		var server = new UdpClient(serverEndpoint);
		while (true) {
			// This struct will contain the info of the sender
			// After calling Receive
			IPEndPoint clientEndpoint = default;
			// Here, we receive a message from some client
			// A ref parameter means, that this function
			// can change the struct from within the function
			var response = server.Receive(ref clientEndpoint);
			Console.WriteLine($"Packet received from: {clientEndpoint} saying: {Encoding.ASCII.GetString(response)}");
		}
	}
}