# Part 4 - Open Word MMO Client:

<img width="958" alt="image" src="https://user-images.githubusercontent.com/7360266/115594257-cf06df80-a2d5-11eb-91ba-225dd29ef2b6.png">

## Preparing the Scene
Create a new Scene named `OpenWordClient` and open it.\
We need a `Canvas`, an Input-`TextField` for the Word And a Send-`Button` to send the Input from the Text-Field when clicked.

## Prerequisites
No new Methods or classes needed.

## Goal
We want to develop a Client in Unity in which we can enter a Word into an Input-T

Now, in Unity, you’ll have to do it the other way round.
Now, you should send a word to your OpenWord-MMO-Port.
To do that, 
You need to create another `UDPClient` on a port of your choice.\
You need to first `Send` Bytes.\
And then `Read` for a response.\
And print the response to your Output.
- `Debug.Log` is fine
- But an Output `TextField` would be nicer.

Remember, that the GameServer only accepts Single Words with less than 20 characters?\
Test, what happens, if you try to enter two words, or a word of 30 characters size?\
What happens, if you send an empty text?\
What do you want to happen?

## Bonus
In the Unity Client, show a Popup-Message whenever an Error was received.\
So, whenever the server decided, that the input was not okay.

