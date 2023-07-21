# Pixel
2D Platform game by Ivan Almaraz, implemented in c# created with Unity

## CS50G's Project: Final
Create a full game from scratch using either LÖVE or Unity.

## Distinctiveness and Complexity
This project is a 2D platformer implemented in c# and created using Unity. Four main parts make up the game, a main menu (the start of the game), levels to play, a pause menu(used when the player hits 'ESC' key), and an end credits scene. The game consists of a character named Pixel that the player must control using the arrow keys to move left and right. The main goal of the game is to make it to the end of each level and proceed until you defeat a boss on the last level. The spacebar key can be pressed to make Pixel jump. The spacebar can also be pressed to make the Pixel jump in the air, this is limited to only one jump while in the air. There are walls and ground that Pixel can use to wall slide. Pixel can jump from the wall and be allowed to jump again. Pixel can die by enemies, spikes, or fallling of the ledge. Pixel consits of a hit box on his feet to be used as a trigger against enemies to kill them.
    
There is three different enemies that can hurt Pixel. A pig that patrols from left to rigth between two points. The pig has an idle state and a walking state each state lasting a random few seconds. A rino that patrols between two points with a idle and running animation, these animation lasting a random few seconds. If the distance between Pixel and the rino are less than a certain amount the rino will attack/charge in the direction of Pixel. When the rino is in the this attack/charging state there is two different points that the rino will stay in between. When Pixel is far enough, the rino will change back to the idle/running animation. The final enemy is a tree trunk enemy that has six points of health. This enemy does not patrol but stands still in an idle state. When Pixel comes close enough the trunk will start shooting seeds (bullets) in Pixel's direction. The trunk will randomly jump and shoot a bullet while in the air. Pixel must avoid these seeds otherwise he will take damage. If Pixel comes a little closer, the trunk will start running in the direction of Pixel, otherwise if the trunk hits Pixel, Pixel will get knocked back a fairly good distance. After defeating the trunk the game proceedes to the end credits scene.

The game has items that Pixel can interact with. There are doors in the game that are closed, the player must maneuver the character to collect a key. 

## What’s contained in Scripts folder
- **AudioManager.cs**:

## How to run application