# GP20-2021-0419-Realtime-Gameserver

## Goal of this Assignment
The goal of this assignment is to introduce you to Game-Server-Development using `C#` and the `TCP` and `UDP` Protocols.\
In total, our steps will include:
- Create a small Time-Server using TCP Technology
- Build a Unity Client to connect to that server and receive the time
- Build a small Word-Game-Server using UDP Technology
- Build a Unity Client to connect to that server and play the game
- Build a Game similar to Agar.io
  - A game server that handles multiple player states
  - A unity client that 
    - can display those players' states
    - and forward any input to the server

## Grading
|Grade  |  Requirement |
|-------|:-------------|
|Summa Cum laude (A*)| Agar.io playable in Multiplayer.\*\*|
| Magna Cum Laude (A)| Agar.io playable with Server. Other players may behave glitchy.\*\*|
|Cum Laude (B)| 6 Points of Agar.io done. (or Bonus for Clean Code\*)|
|Passed (C)| 3 Points of Agar.io done. (or Bonus for Clean Code\*)|
|Barely Passed (D)| UDP OpenWord-MMO Server and Client implemented. |
|Insufficient (E)| TCP Time Server and Client implemented. |
|Failed (F)| |
-------------------------------
Each of these grades expects the previous requirements as well as its own requirements to be fulfilled.\
\* Bonus for Clean Code requires ALL previous requirements to be fulfilled as well as the Code to be tidy and cleaned up.\
\*\* For these grades, it's more important to show that the network communication is happening. Gameplay goes second.


## Prerequisites / Requirements
- Make sure, that .NET Core 5 SDK is installed from https://www.microsoft.com/net/download
- I recommend to use Jetbrains Rider as an IDE.\
- Install Unity Hub & Unity.

## Remarks
- In the first four exercises, we are not using any advanced classes for working with `Streams`
  - it is slightly tedious, but it will teach you about how `byte[]` can be used as buffers
- Refer to the Slides `030 - Internet` for details on TCP / UDP.


## Part 1 - Time Server:

<img width="250" alt="image" src="https://user-images.githubusercontent.com/7360266/115594022-8cdd9e00-a2d5-11eb-8dd3-d9ec6b7ba7c6.png">


### Goal
To have a time-server, where anyone can connect to using TCP to retrieve the current date and time.

### Preparing a Project
Create a folder named `TimeServer`\
Open the Terminal in that Folder
- On Windows, you can Use `cmd` or `PowerShell`.
- On Mac, use the `Terminal`-Application.

One way would be to open the Terminal and type in `cd PATH`, for example: `cd C:/Users/...`.\
Make sure to put quotation marks around the path or to escape it, if you have white spaces in it.

Now, validate, that you are in the correct folder, by using `pwd`\
Now, use the command `dotnet new console`\
If it says `dotnet not found`, you have probably not installed .NET Core 5 SDK, yet.\
Else, this command should have created a new C# Project for you. You can go ahead and open the `.csproj`-File in your IDE.\

Before continuing work, we should create a `.gitignore` in your `TimeServer`-Folder that ignores anything we don't want to commit.\
For C# Console Projects, that's at least the `/bin/` and `/obj/`-Folders.\
You might find a nice C# Console / Rider / Visual Studio `.gitignore`-Template on the web.\
Afterwards, you may safely go ahead and create a new commit `adds time server project`

### Implementation
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



### Considerations:
- What will happen, if you try to listen on a port that is already in use?
- How can you find out, what ports are currently listened on on your computer?
- What could in total go wrong?


## Part 2 - TCP Client:

<img width="361" alt="image" src="https://user-images.githubusercontent.com/7360266/115593918-6a4b8500-a2d5-11eb-9b06-65a67958089b.png">


### Goal
Having a Unity Client that is able to create a TCP Connection to our server.\
In order to request the current Date & Time and Display it to the User.

### Preparing a Project
Create a Unity 2D Project and name it `Agario`.\
We will reuse this project for all of our game server test scripts.\
Do not forget to add a `.gitignore` to this Folder.

### Preparing the Scene
We need a `Canvas`, a `Text` for the Time-Output and a `Button` that the user can click in order to request the time.\

### Implementation
You will need: 
- The `TcpClient`-class which can be created by using its constructor together with arguments for the ip address as well as the port number.
  - `GetStream` again gets you the current stream used for the client. It returns a `Stream`.
  - `Close` needs to be called when you are done using the `TcpClient`.
- The `Stream`-class is returned by `GetStream`
  - `Read` allows you to read Bytes over the socket.
  - `Close` needs to be called when you are done sending bytes over the stream.
- `Encoding.ASCII.GetBytes` Was able to Convert a `string` to `byte[]`.
  - Try to find out, what other method might be able to convert `byte[]` to a `string`. 

Create a class named `RequestServerTime` that inherits from `MonoBehavior` and put said Script on a `GameObject` with the same name.\
Create a public instance method named `SendRequest` and call that method on the `Button`-Component that you have created before.\

Now:
- Use the `TcpClient`-class together with the correct port number (the same port number used in Part 1 on the `TcpListener`)\
= Again, call `GetStream` on that client.\
- On that `Stream`, you can call `Read` to read information.
- It will return `byte[]`, which you need to convert to a `string` again.
  - If you think about how you converted a `string` to `byte[]`, you might come up with a solution to this problem.
- Outut the converted string to the `Text` that you have placed in your scene.
  - Come up with a solution of how to get a reference to said `Text`-Component.


## Part 3 - OpenWord-MMO-Server

<img width="482" alt="image" src="https://user-images.githubusercontent.com/7360266/115595752-9536d880-a2d7-11eb-84d2-21dffa25aa84.png">


### Goal
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

### Preparing a Project
You need to create a new Folder named `./OpenWord-MMO`.\
Then use `dotnet new console` in that directory to create another console project.\
Ahem, `.gitignore`!

### Implementation
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


## Part 4 - UDP Client:

<img width="958" alt="image" src="https://user-images.githubusercontent.com/7360266/115594257-cf06df80-a2d5-11eb-91ba-225dd29ef2b6.png">


### Goal
We want to develop a Client in Unity in which we can enter a Word into an Input-T

Now, in Unity, you’ll have to do it the other way round.
Now, you should send a word to your OpenWord-MMO-Port.
To do that, you need an `Input`-`TextField` And a Send-`Button` to send the Input from the Text-Field.\
When it is pressed:\
You need to create another `UDPClient` on a port of your choice.\
You need to first `Send` Bytes.\
And then `Read` for a response.\
And print the response to your Output.

Remember, that the GameServer only accepts Single Words with less than 20 characters?\
Test, what happens, if you try to enter two words, or a word of 30 characters size?\
What happens, if you send an empty text?\
What do you want to happen?

Bonus: In the Unity Client, show a Popup-Message whenever an Error was received.\
So, whenever the server decided, that the input was not okay.


## Part 5 - Agar.io

For the last part, we are going to implement what we got for implementing our own version of Agar.io

Now, this will be a bit exciting.
We need a lot of features.

The idea of the game is:
The Game has a certain Field Size, e.g. 100x100. `FIELD_SIZE`
All Players need to move within that Field Size. `CLAMP_POSITION`

Now, Players need to be able to Connect. `CONNECT_GAME`
And Spawn in a random location. `PLAYER_SPAWN`, `RANDOM_POSITION`
And Disconnect. `DISCONNECT_GAME`

While they are Connected:
They have the camera attached to themselves. `FOLLOW_CAMERA`
They see themselves as a Circle. `PLAYER_VISUAL`
They are able to Move Around (and pretty much constantly moving). `PLAYER_INPUT`
The players always move towards the Mouse. `MOUSE_POSITION`, `VECTOR_DIRECTION`
But if you are unsure on how to implement that in Unity, you might as well start with WASD controls for now. `WASD_INPUT`

Also, the Game Server spawns small Orbs randomly over the Map (within the Map’s bounds) every x seconds. `SPAWN_ORBS`, `RANDOM_POSITION`, `UPDATE_LOOP`

The players, when touching those orbs, collect them. This will increase their score by one. And the score also increases their appearance (their size). `COLLECT_ORB`, `UPDATE_VISUALS`, `INCREASE_SCORE`

Now, the most interesting part:
Players can eat each other by fully overlapping each other. `DISTANCE_CHECK`
The smaller player gets eaten and starts with a score of zero at a random location again. `PLAYER_RESPAWN`
The larger player gets the other player’s score added to his own. `INCREASE_SCORE`

A few difficult challenges:
How to update a player’s position and score to other players? `OTHER_PLAYERS_BROADCAST` `CURRENTLY_CONNECTED_PLAYERS`
Can you display a small leaderboard? `PLAYER_LEADERBOARD`
Can you show the players’ names on their players? `PLAYER_NAMES`
Can you add Cheat Protection? `CHEAT_PROTECTION`
How do you handle Lags? Do players get stuck and then teleport? Can you somehow `INTERPOLATE`?
