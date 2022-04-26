# Part 1 - Time Server:

<img width="250" alt="image" src="https://user-images.githubusercontent.com/7360266/115594022-8cdd9e00-a2d5-11eb-8dd3-d9ec6b7ba7c6.png">


## Goal
To have a time-server, where anyone can connect to using TCP to retrieve the current date and time.

## Preparing a Project

Create a new .NET Project named TimeServer
- `dotnet new console -o TimeServer`

Initializer a gitignore for that project
- `cd TimeServer && dotnet new gitignore`

Commit
- `git add && git commit -m "Create TimeServer Project"`

## Prerequisites
You will need: 
- The `TcpListener`-class found in `System.Net.Sockets`.
  - `Start` will start the listener.
  - `AcceptTcpClient`-Method handles the acknowledgement of new connections for you. It returns a `TcpClient`.
  - `Stop` needs to be called when you do not want to listen for packets on this port anymore.
- The `TcpClient`-class is returned by `AcceptTcpClient`.
  - `GetStream` gets you the current stream used for the client. It returns a `Stream`.
  - `Close` needs to be called when you are done using the `TcpClient`.
- The `Stream`-class is returned by `GetStream`
  - `Write` allows you to send Bytes over the socket.
  - `Close` needs to be called when you are done sending bytes over the stream.
- `DateTime.Now` Gives you the current Date & Time.
  - `ToString` returns you a nicely formatted `string`.
- `Encoding.ASCII.GetBytes` Can convert a `string` to ASCII-`byte[]` for you.

## Implementation
So, what is our server supposed to do?
- Open a Socket (Listen on a Socket for TCP Messages)
- Then, for as long as you want the server to run (Maybe, start with forever, or rather until you Stop Execution in Rider)
  - Accept a new Client that tries to connect (It will automatically wait for that to happen)
  - Get a data stream from that client that allows Reading and Writing data
  - On that stream, send the current DateTime Encoded into Bytes (You may as well just send `"Hello"` first)
  - Close the stream
  - Close the client

This means, that whenever someone connects via TCP, our Server will send the current Date and Time and close the connection.\
Neat little TimeServer.\
You can Run the Code within Rider using the Play Button.\
Not much will happen, yet, though.\
We need a Client to Connect in order to see, whether everything works.\
If you install `netcat` on Windows, or if you're on a Mac or Linux System:\
You can use `nc -v 127.0.0.1 44444` where `127.0.0.1` is the server's ip and `44444` is the server's port number.
To test your Timeserver.

<img width="706" alt="image" src="https://user-images.githubusercontent.com/7360266/115593725-28224380-a2d5-11eb-9541-548f4f52ce16.png">


## Considerations:
- What will happen, if you try to listen on a port that is already in use?
- How can you find out, what ports are currently listened on on your computer?
- What else could go wrong?