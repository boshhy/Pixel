# Pixel
2D Platform game by Ivan Almaraz, implemented in c# created with Unity

## CS50G's Project: Final
Create a full game from scratch using either LÖVE or Unity.

## Video Link
https://www.youtube.com/watch?v=jnZWjD8w78c

## Distinctiveness and Complexity
This project is a 2D platformer implemented in C# and created using Unity. Four main parts make up the game, a main menu (the start of the game), levels to play, a pause menu(used when the player hits 'ESC' key), and an end credits scene. The game consists of a character named Pixel that the player must control using the arrow keys to move left and right. The main goal of the game is to make it to the end of each level and proceed until you defeat a boss on the last level. The spacebar key can be pressed to make Pixel jump. The spacebar can also be pressed to make the Pixel jump in the air; this is limited to only one jump while in the air. There are walls and ground that Pixel can use to wall slide. Pixel can jump from the wall and be allowed to jump again. Pixel can die by enemies, spikes, or falling of the ledge. Pixel consists of a hit box on his feet to be used as a trigger against enemies to kill them.
    
There are three different enemies that can hurt Pixel. A pig that patrols from left to right between two points. The pig has an idle state and a walking state, each state lasting a random few seconds. A rhino that patrols between two points with an idle and running animation, these animations last a random few seconds. If the distance between Pixel and the rhino is less than a certain amount the rhino will attack/charge in the direction of Pixel. When the rhino is in this attack/charging state there are two different points that the rhino will stay in between. When Pixel is far enough, the rhino will change back to the idle/running animation. The final enemy is a tree trunk enemy that has six points of health. This enemy does not patrol but stands still in an idle state. When Pixel comes close enough the trunk will start shooting seeds (bullets) in Pixel's direction. The trunk will randomly jump and shoot a bullet while in the air. Pixel must avoid these seeds otherwise he will take damage. If Pixel comes a little closer, the trunk will start running in the direction of Pixel, otherwise if the trunk hits Pixel, Pixel will get knocked back a good distance. After defeating the trunk, the game proceeds to the end credits scene.

The game has items that Pixel can interact with. There are doors in the game that are closed, the player must maneuver Pixel to collect a key. Once Pixel has collected a key, the door can be opened. The player can push the up arrow to enter the door and proceed to the next level. Another item is the check points, Pixel can come into contact with a check point and an animation will play to let the player know that this is where Pixel will spawn in case of a death. There are floating platforms that Pixel can jump on. Once Pixel is on, the platform will move up and down between two points. Once Pixel jumps off the platform will return to its starting location. A Heart is another item that Pixel can pick up to gain one health point. If Pixel runs into spikes he will be hurt and get knocked back. Trampolines will cause Pixel to bounce in the air, Pixel will have the ability to double jump in the air every time he jumps off a trampoline.

**Problems**: Pixel must be touching the ground to be able to do a normal jump and then double jump. I tried to play around with the player being able to fall off a ledge and be able to do both jump. I did accomplish this but the function in the trampoline script did not want to take away the regular jump. I think this is due to the way Unity runs scripts, I was not able to fix this problem, so I kept it the way it is now. If I have time I would go back to change it because it does make for a better player experience. I also implemented some strawberries items that could be picked up but decided to scratch that idea because it doesn't add anything to the game, sure I could add how many have been collected but doesn't serve any purpose. The floating platform sometimes glitches out when returning to their original location, added a range value for the platform and it seemed to fix the problem. 

## What’s contained in Scripts folder
- **AudioManager.cs**: Used to manage the audio, this can be called with an index to play a specified sound.
- **BossController.cs**: Used to control the movements of the 'Trunk' boss.
- **BossHealthController.cs**: Used to keep track of boss health.
- **BossUIController.cs**: Used to control the Canvas drawing the hearts for the boss.
- **bullet.cs**: Used to control a bullet that has been instantiated.
- **CameraController.cs**: Used as camera view to follow player.
- **checkPoint.cs**: Used to activate the checkpoint touched.
- **CheckPointController.cs**: Used to set spawnpoint and activate or deactivate checkpoints.
- **DamagePlayer.cs**: Used to deal damage to player.
- **DamagePlayerLeft.cs**: Used to deal player damage by a boss object from left side.
- **DamagePlayerRight.cs**: Used to deal player damage by a boss object from right side.
- **DamagePlayerWithRino.cs**: Used to deal damage to player with rino object.
- **DestroyOverTime.cs**: Used to Destroy an instantiated effect.
- **DoorController.cs**: Used to control the door.
- **HitBox.cs**: Used as a hitbox on the bottom of players feet, for jumping on enemies.
- **hurtBoss.cs**: Used to hurt the enemy boss.
- **itemPlatform.cs**: Used to control a moving platform.
- **KeyController.cs**: Used to collect the key.
- **KillPlayer.cs**: Used to kill the player if they fall out of world.
- **LevelLoader.cs**: Used to transition from scene to scene.
- **LevelManager.cs**: Manages player respawn when loadding a scene or when player dies.
- **MainMenu.cs**: Used for the menu at the beginning of the game.
- **MovementController.cs**: Used to control player movement and animations.
- **PauseMenu.cs**: Used to control the pause menu.
- **Pickup.cs**: Used to pick up objects.
- **PigController.cs**: Used to control the pig.
- **PlayerHealthController.cs**: Used to keep track of player health.
- **RinoController.cs**: Used to control the rino character.
- **Trampoline.cs**: Used to control the trampoline.
- **UIController.cs**: Used to control the Canvas drawing the hearts for the palyer.


## How to run application
Download all folders and files, open with Unity and hit play. Players can also build the game and run it that way. Resolution in mind when making the game was 1920x1080.
 