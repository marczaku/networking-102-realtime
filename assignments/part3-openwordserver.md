# Part 3 - OpenWord MMO Server

<img width="482" alt="image" src="https://user-images.githubusercontent.com/7360266/115595752-9536d880-a2d7-11eb-84d2-21dffa25aa84.png">


## Goal
Creating an Open Game Server for a small Word Game.\
Players can send Words to the Server that builds them into sentences and sends the full sentence back.\
Clients do not have to explicitly connect to the Game. They can simply participate by sending data to the Server.\

Rules:
- The server can receive any segments sent to its Port via UDP.
  - It only allows a single word to be sent at a time.
    - How can you validate, that only one word was sent?
  - Also, it only allows words to have up to 20 characters per word.
    - How can you validate, that the word is not too long?
- It remembers the text that was sent before and adds the new word, that was sent just now, after a whitespace behind it.
And it every time sends the whole text back to the client.
  - ClientA: -> "Hi" -> Server -> "Hi" -> ClientA
  - ClientB: -> "Welcome" -> Server -> "Hi Welcome" -> ClientB
  - ClientA: -> "World" -> Server -> "Hi Welcome World" -> ClientA

## Preparing a Project
Create a new .NET Project named TimeServer
- `dotnet new console -o OpenWord-MMO`

Initializer a gitignore for that project
- `cd OpenWord-MMO && dotnet new gitignore`

Commit
- `git add && git commit -m "Create OpenWord-MMO Project"`

## Prerequisites
You will need:
- The `UdpClient`-class which can be found in `System.Net.Sockets`.
  - The constructor in which you can pass a port number. 
  - The `Receive(ref remoteEndPoint)`-Method to receive data from that socket.
    - The return type is `byte[]` and gives you the information that was received.
    - The `ref remoteEndpoint`-Argument will be filled by the method to contain the EndPoint (IP+Port) that has sent you the packet.
    - The value that `ref remoteEndpoint` when you give it into this method does not matter.
  - The `Send(bytes, bytesLength, remoteEndpoint)`-Method to send data on that socket.
    - `bytes` and `bytesLength` contain a `byte[]` of your data that you want to send, as well as the length of said array.
    - `remoteEndpoint` should be the address of the remote that you want to send data to.

## Implementation
What is your server supposed to do?
- Store the complete message in a variable.
- Create a `UdpClient` with a Port of your choice.
- While you want the server to run (maybe, forever? Then use `true`)
  - `Receive` data from that `UdpClient`
  - Validate the data that you have received according to your server's rules:
    - only one word, not longer than 20 characters, ...
  - If it is not valid, handle the error somehow.
  - If it is valid:
    - Add the new word with a whitespace to your complete message.
    - `Send` your complete message over your `UdpClient` by passing in your message converted to bytes. And sending it to the `IPEndPoint` received by the `Receive`-Method as a `ref`-Parameter.
- `Close` everything when we're done.

Details:\
You need to create a new `System.Net.Sockets.UdpClient` and pass it a port number that you want to use.\
For example `11000`.\
Now, the way, this API works, is:\
```cs
var remoteEP = new IPEndPoint(IPAddress.Any, 11000); 
var data = udpClient.Receive(ref remoteEP);
```

The IP filters, what IP addresses we want to filter on.\
It is passed as a ref parameter, so that the Receive Function can change its value.\
When we receive data, the remoteEP will actually contain the IP Address and Port Number of whoever just sent us bytes.\
Therefore, we can use it to send back information.

To respond, you can use `udpClient.Send(bytes, bytesLength, remoteEP);` to send a response.

Do this all endlessly in a loop and your Server should run until the end of our solar system :)

Well, our UDP Server is standing.
You can Run it using the Play Button in Rider.

Again, it won't do much, until someone sends us info.
You can use `nc -u 127.0.0.1 11000` in the Terminal to connect to your server and send text.
