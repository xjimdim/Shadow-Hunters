![Shadow Hunters RPG Card Game by XjiMDim](https://github.com/xjimdim/Shadow-Hunters/blob/master/screencaptures/3.png) 
***


## What is Shadow Hunters?

Shadow Hunters is a famous RPG Card board game by Yasutaka Ikeda. 

The whole point of the game is to identify your opponents or allies! You are a Werewolf, a Vampire or a human who just caught in the middle of this ancient battle. But your identity remains secret until someone outs you through your actions and through Hermit cards or you reveal yourself to use your special ability. The key to victory is to identify your allies and enemies early. Survival! Once your identity is revealed, your enemies will attack without impunity using their special abilities or their equipment. This ancient battle comes to a head and only one group will stand victorious (or a civilian, in the right circumstances). Check out the instructions of the game here: https://dimitriskalogirou.com/shadowhunters/Instructions.html

## How to run
In order to run this game (https://apps.facebook.com/theshadowhunters), you need at least 2 Facebook accounts because when you get in a room you can only click play if there are 2 or more players ready in the room (I think it makes sense :P). 

WARNING: If you want to run this game in Unity editor you need to be a test user in the Facebbok Application so you can take an Access Token from this address https://developers.facebook.com/tools/accesstoken/?app_id=1572529873014800 . If you want to do that please contact me (http://m.me/XjiMDim or xjimdim@gmail.com)  and give me your facebook info and I will happily add you as a Test User in the official Facebook Application. 

## Basic Scripts

| Script Name | Description |
| --- | --- |
| GameManager.cs | This is the main script of the game. All basic functionality is developed in this script, from drawing a card to Game over. Also the majority of other scripts communicate with this one through the instance variable.  |
| NetworkManager.cs  | This script is responsible for the start of the game (it is practically the GameManager of the _start scene). From room creation to loading the actual game (_project scene) and it is also resposible for all possible network senarios (connection - disconnection of a player, server status display etc.). This is the only script that remains in-game after the _start scene is destroyed |
| FBHolder.cs | This script uses Facebook SDK for Unity. It includes functions that register the player to the game using Facebook, display user's Facebook Profile Picture and their name in game and gives the ability to Invite other Facebook users or Share the game  |
| SDieValue.cs and FSDieValue.cs | Those scripts are resposible for handling the result of a dice roll (for the two seperate dice, 6sided and 4sided), after they have been rolled from the GameManager script.  |
| GreebCardScript.cs and RedCardScript.cs and BlueCardScript.cs | After the creation of the cards, every card has one of those scripts attatched to it's gameobject. Evey one of those scripts is resposible for running the rules of each card (depending on type: Green, Red or Blue). They are also resposible for adding a card to player's equipment (if this card is an equip card)  |
| Tile.cs | This script is resposible for finding an empty spot each time a player pawn has to move to a new region. It is also resposible for emptying a spot after a pawn has moved from it.  |

## What is completed by now (v0.9)?
At this moment pretty much everything is completed as far as functionality goes and I am just working out on some minor bugs during the end of the game. Facebook Integration runs smoothly and a game of up to 5 players is possible. 

## What's next?
Make the game prettier: During development I mostly focused on how to make the game work and didn't give much attention to how it will look like. So, one of my plans it's to make the whole game prettier (for both the _start scene and the _project scene)

Sound: The game is pretty silent now. There will be sound in one of the future updates.

User Points and User Rewards: One of my plans for the future of this game is to implement some kind of reward system for the winners so that the user whould have a reason to come back.

Go Mobile: This game is mostly point and click, so a mobile implementation could be a good move  

## Some screens of the game (v0.9): 

Lobby:
![Lobby Shadow Hunters Unity](https://github.com/xjimdim/Shadow-Hunters/blob/master/screencaptures/1.png)

Room:
![Shadow Hunters by XjiMDim](https://github.com/xjimdim/Shadow-Hunters/blob/master/screencaptures/2.png) 

Ingame:
![Shadow Hunters by XjiMDim](https://github.com/xjimdim/Shadow-Hunters/blob/master/screencaptures/3.png)
 
Picking up card after going to the right region:
![Shadow Hunters by XjiMDim](https://github.com/xjimdim/Shadow-Hunters/blob/master/screencaptures/4.png)
 
