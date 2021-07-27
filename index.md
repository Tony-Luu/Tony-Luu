<p align="center">
  <b>Contacts</b>
</p>
{% include index.html %}
{% include social-media-links.html %}

Hello there! Thanks for stopping by to check out my games porfolio. I am actively seeking a developer position at the moment so if you're interesting in what I can do then please check out my contacts and socials to discuss an opportunity with me. 
<br>
<p align="center">
  <b>About Me</b>
</p>
I'm a Computer Games Development graduate from University of East London with a passion for games development and programming. This portfolio demonstrates a variety of gameplay features implemented in these projects to generate interesting gameplay. My passion for gaming first began since childhood as the first few games I have ever played
were Pokemon Gold, Final Fantasy 4, and Age of Empires. From that point onwards, it has
shaped my tastes for those genres. I eventually discovered free hobbyist game creation
software such as RPG Maker 2000 and Game Maker. This got me into games development
because I have thoroughly enjoyed the freedom and expression of creating unique gameplay
experiences.

The timestamps shown below the gameplay footage of my projects demonstrates the kind of gameplay I like to focus on developing and showcases my skills in Unity.


## ~~H~~armless
Game Engine: Unity\
Language: C#\
Platform: PC and Web Browser\
Duration: 11/7/21 - 25/7/21

<video body="" controls="" controlslist="nodownload" oncontextmenu="return false" style="display: block; margin: 0 auto;" width="830">
  <source src="https://github.com/Tony-Luu/HTML5-Videos/blob/master/Armless%20Gameplay%20Footage.mp4?raw=true" type="video/mp4"></source>
  Your browser does not support HTML5 video.
</video>

Unity WebGL Player: [Link](https://tony-luu.github.io/Armless/){:target="_blank"}\
Download: [Link](https://www.dropbox.com/s/ijw5i0osr8hytuo/Armless%20PC%20Build.zip?dl=0){:target="_blank"}\
Source Code: [Link](https://github.com/Tony-Luu/Game-Parade-Summer-2021/tree/main/Assets/TL_Scripts){:target="_blank"}

~~H~~armless is a first-person puzzle game made during the Game Parade Summer 2021 hosted from a Discord community called Games Jobs Live which is a games development community. The theme for this gamejam was "One Time Use" and I teamed up with six other people composed of a programmer, two 3D artists, one 2D artist, and sound designer. The first week was creating the game and the second week was to polish the game such as tweaking the gameplay, fixing bugs, and adding sounds.

- At 00:08, the detaching of arms was created by unparenting the arms from the player character.

- The player character regains their arms from the barrel filled with extra limbs at 00:17.

- At 01:13, the arms from the player character gets detached from the character and attached to the wooden beam by adding a hinge joint onto the detached arm.

- The detached arms are capable of attaching to other detached arms by adding a hinge joint onto the arm at 01:20.

- At 01:44, the player character holds onto the arm rope and swings on it by adding a hinge joint onto the character and jumps off the arm rope by removing the hinge joint.


## Polar
Game Engine: Unity\
Language: C#\
Platform: PC\
Duration: 21/2/21 - 28/2/21

<video body="" controls="" controlslist="nodownload" oncontextmenu="return false" style="display: block; margin: 0 auto;" width="830">
  <source src="https://github.com/Tony-Luu/HTML5-Videos/blob/master/Polar%20Gameplay%20Footage.mp4?raw=true" type="video/mp4"></source>
  Your browser does not support HTML5 video.
</video>

Download: [Link](https://www.dropbox.com/s/but0b7fma4zblec/Polar%20PC%20Build.rar?dl=0)\
Source Code: [Link](https://github.com/Tony-Luu/Polar-Sourcecode){:target="_blank"}\
Problems and Solutions: [Link](https://github.com/Tony-Luu/Games-Portfolio/blob/gh-pages/Polar%20Problems%20and%20Solutions.md){:target="_blank"}

#### Note: Due to the project using Wwise Unity, it is incompatible with WebGL so therefore there is no WebGL build. Instead, a download link containing a PC build is provided for those who are interested in playing the game.

Polar is a 3D platforming game made during the Game Parade Spring 2021 hosted from a Discord community called Games Jobs Live which is a games development community. The theme for this particular gamejam was "Strength lies in differences" and I teamed up with four other people composed of a programmer, artist, designer, and sound designer.

- At 00:09, the jumping was made by adding velocity to the Y axis on the rigidbody of the character.

- At 00:18, the dash ability was created by adding force in the forward direction on the rigidbody. The dash also had a special property of only dashing through blue panels by changing the layer of the character to strictly dash through them.

- Switching characters at 00:20 was created by swapping out the character model and the hair. The scripts of the abilities for the character were swapped out by disabling the scripts of the blue character and enabling the scripts of the red character.

- The shoulder tackle ability at 00:23 is very similar to the dash ability the blue character with the exception that the red character's layer is changed to strictly collide with the crates with the red character's rigidbody.

- The grabbing ability at 00:32 was created by a trigger collider and a key press that parents the picked up object onto the character's hand.

- The throwing ability at 00:34 used add force on the rigidbody of the picked up object in the forward direction of the character and unchilded the picked up object.


## Calor
Game Engine: Unity\
Language: C#\
Platform: PC\
Duration: 27/11/20 - 6/12/20

<video body="" controls="" controlslist="nodownload" oncontextmenu="return false" style="display: block; margin: 0 auto;" width="830">
  <source src="https://github.com/Tony-Luu/HTML5-Videos/blob/master/Calor%20Gameplay%20Footage.mp4?raw=true" type="video/mp4"></source>
  Your browser does not support HTML5 video.
</video>

Unity WebGL Player: [Link](https://tony-luu.github.io/Calor/){:target="_blank"}\
Source Code: [Link](https://github.com/Tony-Luu/Calor-Sourcecode){:target="_blank"}\
Problems and Solutions: [Link](https://github.com/Tony-Luu/Games-Portfolio/blob/gh-pages/Calor%20Problems%20and%20Solutions.md){:target="_blank"}

Calor is a 2D game project made during the Winter GameJam 2020 hosted from a Discord community called Game Dev London which started as a games development meetup in London many years ago. This gamejam lasted for approximately 40 hours throughout one week. The theme for the gamejam was called "Warmth" so I teamed up in a group of 5 to brainstorm various ideas associated with the word to generate interesting ideas. 

- The animation for the character at 00:07 was created using the animation tool in Unity to animate the walking animation frame by frame then I had to use the animator to create transitions to trigger that animation. I had to create a Finite State machine to manage the conditions for all of the animations shown in the video such as: jumping, landing, swinging the torch, dragging and pushing the crate. I also included sound effects for the walking and jumping animations.

- At 00:14, I created the torch by using a particle effect attached with a fire texture on it.

- I coded an audio player to change the background music at 00:22 whenever the player enters a new area.

- The campfire being lit by the character's torch swing at 00:30 is a checkpoint so when the character dies, it will respawn at that point.

- The hanging vine being set alight at 00:46 was made by a particle collision which triggers a flaming particle effect which follows the burning vine. The vine shrinks while the flaming particle effect follows the direction of the vine which melts the icicles that makes them fall down into the dark pit. This provides a platform for the player to reach the other side of the area.

- At 1:01, the character's torch swing melts the block of ice was made by using particle collision which reduces the scale of the ice blocks.

- An ending video is played at 1:06 once the player reaches the end of the ice cave and loops back to the beginning of the game once the ending video finishes.


## Infinite Defender
Game Engine: Unity\
Language: C#\
Platform: Android devices\
Date of publication: 7/7/17

<video body="" controls="" controlslist="nodownload" oncontextmenu="return false" style="display: block; margin: 0 auto;" width="830">
  <source src="https://github.com/Tony-Luu/HTML5-Videos/blob/master/Infinite%20Defender%20Gameplay%20Footage.mp4?raw=true" type="video/mp4"></source>
  Your browser does not support HTML5 video.
</video>

Unity WebGL Player: [Link](https://Tony-Luu.github.io/Infinite-Defender/){:target="_blank"}\
Source Code: [Link](https://github.com/Tony-Luu/Infinite-Defender-Sourcecode){:target="_blank"}\
Problems and Solutions: [Link](https://github.com/Tony-Luu/Games-Portfolio/blob/gh-pages/Infinite%20Defender%20Problems%20and%20Solutions.md){:target="_blank"}

Infinite Defender is a throwback to the old-fashioned arcade space-shooting games with a twist. Enemy waves are procedurally-generated so they spawn differently every time you play it. The waves continuously spawn until the player dies so survive as long as you can and earn a high score. This 2D project was made solo and it is intended for the Android devices and smartphones. 

- The moving and shooting at 00:06 was made to move the ship towards where the player has touched on the screen. If the player touched on the left side of the screen then the ship moves towards the left. If the player touched the right side of the screen then the ship moves towards the right.

- The enemies spawning in at 00:13 had movement in a shape of an arc which was made by using a function from transform called RotateAround.

- At 00:37, the enemies that move in a circular motion was achieved by using the Cosine and Sin math functions.

- At 1:04, enemies have a small chance of dropping a random power-up that can boost the player's attributes such as: attack speed, movement speed, defense, shield up, and base health up. The ship icon with a shield around it coloured green is the shield up boost.


<p align="center">
  <a href='https://play.google.com/store/apps/details?id=com.None.InfiniteDefender&pcampaignid=pcampaignidMKT-Other-global-all-co-prtnr-py-PartBadge-Mar2515-1' target="_blank"><img alt='Get it on Google Play' src='https://play.google.com/intl/en_us/badges/static/images/badges/en_badge_web_generic.png' height="100"/></a>
</p>


## Lemuria: Land of Lost Treasures
Game Engine: Unity\
Language: C#\
Platform: Android devices\
Date of completion: 24/1/17

<video controls="" controlslist="nodownload" oncontextmenu="return false" style="display: block; margin: 0 auto;" width="830">
  <source src="https://github.com/Tony-Luu/HTML5-Videos/blob/master/Lemuria%20Gameplay%20Footage.mp4?raw=true" type="video/mp4"></source>
  Your browser does not support HTML5 video.
</video>

Unity WebGL Player: [Link](https://tony-Luu.github.io/Lemuria/){:target="_blank"}\
Source Code: [Link](https://github.com/Tony-Luu/Lemuria-Sourcecode){:target="_blank"}\
Problems and Solutions: [Link](https://github.com/Tony-Luu/Games-Portfolio/blob/gh-pages/Lemuria%20Problems%20and%20Solutions.md){:target="_blank"}

This is a 2D adventure game where the player has to explore around the world to collect rare treasures and collectibles. The challenges contained in this game are grid-based. This project was made by four people. This game was intended for Android devices as well as smartphones. Click on the link above to read more about how I devised solutions for the problems I have encountered.

- The sokoban puzzle at 00:11 was procedurally generated by carving out the level area with using 3 by 3 templates with different layouts to differentiate the area.

- The spike trap maze at 00:35 was created with using a Depth First Search algorithm to create a maze with no deadends to provide multiple paths for the player to reach the exit. The spikes were randomly generated in the pathway and the algorithm tunneled out the maze when the level area is intially generated with walls.

- The doppelganger event at 1:11 had a sequence puzzle where you have to match the flashing symbols on the ground with the doppelganger in order. The doppelganger moves opposite of your character.

- The combat at 1:26 is grid-based where the player has to move the character's attack range indicated by the red boxes onto the enemy to attack and defeat the monster.


## Nanobots
Game Engine: Unity\
Language: C#\
Platform: PC\
Date of completion: 12/1/15

<video controls="" controlslist="nodownload" oncontextmenu="return false" style="display: block; margin: 0 auto;" width="830">
  <source src="https://github.com/Tony-Luu/HTML5-Videos/blob/master/Nanobots%20Gameplay%20Footage.mp4?raw=true" type="video/mp4"></source>
  Your browser does not support HTML5 video.
</video>

Unity WebGL Player: [Link](https://tony-luu.github.io/Nanobots/){:target="_blank"}\
Source Code: [Link](https://github.com/Tony-Luu/Nanobots-Sourcecode){:target="_blank"}\
Walkthrough: [Link](https://github.com/Tony-Luu/Games-Portfolio/blob/gh-pages/Nanobot%20Walkthrough.md){:target="_blank"}

This project was made as my coursework for one of my modules. Nanobots is a 3rd person puzzle platforming game where the player has to traverse through the level using nanobots. My lecturer had a discussion about using dynamic elements such as particle effects, physics, textures, lights, and sound in an unconventional way. I used particle effects to represent the different functions of nanobots. The walkthrough for this game is at the link above this description.

- At 00:03, I used a particle effect to collide with the destructible obstacle to reduce the size of it until they are destroyed.

- At 00:48, the particle effect I have used has a different function called "Hook" which lets the player pull towards the hook. The cube object can be picked up and placed on buttons as a weight, block laser particles or used as an extra platform.

- At 2:31, I used textures to implement this memory puzzle which randomly creates a sequence of flashing colours which the player must click on the correct sequence to progress through. The material for the flashing textures uses a normal map, a height map, and illumin properties to create the sequence of flashing colours.

- At 2:52, the torch light uses a raycast that hits the invisible platform on the ground which decreases the transparency of the material on the platform.


## Lunar Odyssey
Game Engine: Unity\
Language: C#\
Platform: Android devices\
Date of completion: May 2015

<video controls="" controlslist="nodownload" oncontextmenu="return false" style="display: block; margin: 0 auto;" width="830">
  <source src="https://github.com/Tony-Luu/HTML5-Videos/blob/master/Lunar%20Odyssey%20Gameplay%20Footage.mp4?raw=true" type="video/mp4"></source>
  Your browser does not support HTML5 video.
</video>

Unity WebGL Player: [Link](https://tony-luu.github.io/Lunar-Odyssey/){:target="_blank"}\
Source Code: [Link](https://github.com/Tony-Luu/Lunar-Odyssey-Sourcecode){:target="_blank"}

This game was created by 7 members for a group project during university. Lunar Odyssey is a 2D endless survival game where the player has to avoid obstacles created by the Greek gods. Use the mouse to drag the moon vertically up and down to control the ocean waves and drag the boat left to right would move the boat horizontally. I have used what I have learned from the module called Games Studio to implement obstacles by using Unity components such as:

- The volcano spewing up fiery rocks at 0:49 had the rigidbody component attached to it to fall from the sky and collide with the ship which causes damage if it hits.

- The cyclops picking up the boulder and throwing it above himself at 1:40 uses the rigidbody component to animate the boulder being picked up and thrown at the ship would collide with the ship if it hits.

- The kraken's tentacle attack at 2:27 has both a collider and a rigidbody component which can cause damage if the tentacles hit the ship.

- The lightning cloud at spawned at 3:18 has a particle effect with a lightning texture attached to it which can collide with the ship.
