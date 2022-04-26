# Part 5 - Agar.io

## Goal
For the last part, we are going to implement our own version of [Agar.io](http://agar.io). You should have all the required knowledge already!

## Overview
We need a lot of features.

The idea of the game is:
- The Game has a certain Field Size, e.g. 100x100. `FIELD_SIZE`
- All Players need to move within that Field Size. `CLAMP_POSITION`

Now, Players need to be able to Connect. `CONNECT_GAME`
- And Spawn in a random location. `PLAYER_SPAWN`, `RANDOM_POSITION`
- And Disconnect. `DISCONNECT_GAME`

While they are Connected:
- They have the camera attached to themselves. `FOLLOW_CAMERA`
- They see themselves as a Circle. `PLAYER_VISUAL`
- They are able to Move Around (and pretty much constantly moving). `PLAYER_INPUT`
- The players always move towards the Mouse. `MOUSE_POSITION`, `VECTOR_DIRECTION`
- But if you are unsure on how to implement that in Unity, you might as well start with WASD-controls for now. `WASD_INPUT`

Also, the Game Server spawns small Orbs randomly over the Map (within the Map’s bounds) every x seconds. `SPAWN_ORBS`, `RANDOM_POSITION`, `UPDATE_LOOP`

The players, when touching those orbs, collect them. This will increase their score by one. And the score also increases their appearance (their size). `COLLECT_ORB`, `UPDATE_VISUALS`, `INCREASE_SCORE`

Now, the most interesting part:
- Players can eat each other by fully overlapping each other. `DISTANCE_CHECK`
- The smaller player gets eaten and starts with a score of zero at a random location again. `PLAYER_RESPAWN`
- The larger player gets the other player’s score added to his own. `INCREASE_SCORE`

And to make it actually work for multiple players:
- How to update a player’s position and score to other players? `OTHER_PLAYERS_BROADCAST` `CURRENTLY_CONNECTED_PLAYERS`

A few difficult challenges:
- Can you display a small leaderboard? `PLAYER_LEADERBOARD`
- Can you show the players’ names on their players? `PLAYER_NAMES`
- Can you add Cheat Protection? `CHEAT_PROTECTION`
- How do you handle Lags? Do players get stuck and then teleport? Can you somehow `INTERPOLATE`?

## Preparing the Scene
Create a new Scene named `Agario` and open it.\
Do what needs to be done ;)

## Prerequisites
- Create one or more Sequence Diagrams that show what your server and your clients are communicating to each other.
  - Especially for Connect, Disconnect and Position Update
  - Decide, what Protocols you want to use
    - What would be the choice for best performance?
    - What's the easiest choice?
- Implement a Model for the Player on the Server
  - What information do you need to store per player?
  - Use maybe a List or a Dictionary to hold all connected players
- Implement a Model for the Orbs on the Server
  - What information do you need to store per orb?
  - Use maybe a List or a Dictionary to hold all spawned orbs
