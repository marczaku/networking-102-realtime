# Part 2 - Time Client:

<img width="361" alt="image" src="https://user-images.githubusercontent.com/7360266/115593918-6a4b8500-a2d5-11eb-9b06-65a67958089b.png">


## Goal
Having a Unity Client that is able to create a TCP Connection to our server.\
In order to request the current Date & Time and Display it to the User.

## Preparing a Project
Create a Unity 2D Project and name it `Agario`.\
We will reuse this project for all of our game clients across all assignments.\
Do not forget to add a `.gitignore` to this Folder.

## Preparing the Scene
Create a new Scene named `TimeClient` and open it.\
We need a `Canvas`, a `Text` for the Time-Output and a `Button` that the user can click in order to request the time.\

## Prerequisites
You will need: 
- The `TcpClient`-class which can be created by using its constructor together with arguments for the ip address as well as the port number.
  - `GetStream` again gets you the current stream used for the client. It returns a `Stream`.
  - `Connect` connects to a tcpListener. Enter the ipaddress and port number of the TimeServer's TcpListener here.
  - `Close` needs to be called when you are done using the `TcpClient`.
- The `Stream`-class is returned by `GetStream`
  - `Read` allows you to read Bytes over the socket.
  - `Close` needs to be called when you are done sending bytes over the stream.
- `Encoding.ASCII.GetBytes` Was able to Convert a `string` to `byte[]`.
  - Try to find out, what other method might be able to convert `byte[]` to a `string`. 

## Implementation
Create a class named `RequestServerTime` that inherits from `MonoBehavior` and put said Script on a `GameObject` with the same name.\
Create a public instance method named `SendRequest` and call that method from the `Button`-Component that you have created before.

Now:
- Use the `TcpClient`-class together with the correct port number (the same port number used in Part 1 on the `TcpListener`)\
= Again, call `GetStream` on that client.\
- On that `Stream`, you can call `Read` to read information.
- It will return `byte[]`, which you need to convert to a `string` again.
  - If you think about how you converted a `string` to `byte[]`, you might come up with a solution to this problem.
- Output the converted string to the `Text` that you have placed in your scene.
  - Come up with a solution of how to get a reference to said `Text`-Component.