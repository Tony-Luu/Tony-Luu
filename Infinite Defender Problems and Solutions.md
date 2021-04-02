The process of balancing the gameplay was tweaking the difficulty spike of the game. It needed to feel that the enemies spawned into the level was not becoming difficult too 
ast or too slow. The decision made was to gradually increase the movement and attack speed of the enemy spaceships to ensure that the player can learn and study how the certain 
enemy types move.

One of the design iterations I have made in this project are the controls. Initially, the player can only move and shoot left and right by touching the left and right side of 
the screen respectively. After some play testing, it felt really hard to aim because you can only move when you shoot and you can only shoot when you move. This created some 
dissonance between moving and shooting. The solution was to implement the player's spaceship to stand still when the player touches both the left and right side of the screen at
the same time. This solved the problem with aiming and it also makes sense. Force equally applied to both opposite directions would cause the position at the center to not move.


## Sine Wave Motion
After the previous project stopped indefinitely due to unforeseen circumstances, I have started another project on my own. It will be a throwback to the old-fashioned arcade 
style space shooting game but with a twist. I have begun creating a variety of movement patterns when the enemies spawn and the one I will like to make in particular is the one 
moving in a snake-like formation just like in those old space shooting games. I have looked into how to create that kind of movement and it was done by using a maths function 
called Sin which is one of the formulae of Trigonometry.

## The Problem and Solution
One of the issues I had with creating the sine wave motion was whenever the enemy is being spawned, it was not being spawned in the correct position. It was being spawned at 0 
in the Y position. The solution is to record the initial position of the NPC with a Vector 2 variable and then use that variable to add with the X and Y positions.

## First Iteration
I have finished the control and mechanics of the game and made a prototype build for it. I have not tested the build on an Android device before so this is the first time in 
doing so and encountered various problems as I have got my friends to play it. The power ups and weapon upgrades were only rewarded to players if they have completed an entire
wave with the base unharmed (which is off-screen). One of my friends mentioned that they were no power ups when he played the game. This made it clear that there was no 
indication to the player beforehand that you get rewarded power ups and weapon upgrades when you complete a wave with the base unharmed. The solution was to make the enemies 
drop power ups when the player's spaceship destroys them.

Another issue one of my friends mentioned was that there were no boss encounters whatsoever. The problem was that it left out an essential challenge that existed in current 
arcade space shooting games. The solution was the include boss encounters with each of them having a unique ability that differentiates each other once every couple of waves.
One of my friends asked me if there was a way to pause the game. Since there was no way to pause the game, I have decided that the timer displayed at the top of the screen will
be the pause button.

When I have tested the game myself on my own Android device, I have realized that the increased speed from every new wave was quite fast. I have a lot of trouble with keeping up
with the enemies per wave. The problem was that the difficulty spiked too fast. The solution was to reduce the increased speed of the enemies per wave. Another problem I have 
encountered when I played my build on the Android device was that there was no way to stand still and shoot at the same time to aim. The solution was to implement an if 
statement detecting whenever there was two touches at both of the left and right sides of the screen and if there was then freeze the spaceship's movements.

The touch movement from pressing the left to right sides of the screen has been commented by my friends about the auto-attacking while moving at the same. One suggestion 
mentioned was to let the spaceship constantly shoot so there was no need to shoot while moving. Another suggestion was to have a toggle button on the screen for the player to 
decide how the spaceship would shoot. Ultimately, I decided to keep the shooting the way it was as I felt comfortable with it.

Throughout playing the game I have noticed that the enemy spaceships projectiles were as fast as the player's spaceship projectiles. This caused a problem with the player's 
spaceship moving into the enemy spaceship's line of fire. The solution was to slow down the enemy projectiles to give the PC a chance to avoid enemy fire while firing at the 
enemy.


## Second Iteration
After taking in a considerable amount of comments, opinions, and feedback about the current state of my build, I have made vast improvements since my last iteration. 
Unfortunately, there are still issues that are encountered during the second iteration of my build when I have let my friends play my build. One of the issues encountered was 
that the attack speed power up was too strong for the player's spaceship as it doubled the firing rate.

This created a huge balancing problem when the player's spaceship obtains the side guns upgrade from completing a wave with the base unharmed. Combined with the power up, 
it can clear waves easily regardless of how fast the enemies or bosses are. The solution is to reduce the increased firing rate upon obtaining it. Another suggestion made for 
the power ups is to introduce a power up that grants the player spaceship's projectile to move in a different pattern.

One of the comments I have received was when the enemies appear from the sides of the screen, the enemy spaceships just shoot downwards. This causes a problem where the player's
spaceship can just stay in one position and just destroy them all without moving an inch. So I decided to make the enemies look at the player's spaceship and shoot towards it to
resolve the problem.

Another issue one of my friends mentioned was that there was no indication of the player's spaceship getting hit by the enemy projectile. This caused a problem where the player 
has no idea when the spaceship was being damaged. The solution was to create a flashing shield around the spaceship which indicates to the player that the shields are getting 
damaged. I decided to let my previous lecturer play my game and mentioned that there should be changing environments such as asteroids or floating mines to keep the game 
interesting.

One of the comments one of my friends made was that their fingers obstructed the view of the player's spaceship. This caused a problem with visibility as they cannot see where 
they are properly moving the spaceship. The solution was to move the player's spaceship above the bottom of the screen to indicate that space allows the player to move the spaces
hip without any obstruction.

An observation I have made from my friends playing the game was that they could not see when they are being damaged despite the fact there is a text and number indicating both 
the player's health and the health of the base. In the next iteration, I plan to implement health bars of changing colors (green when the health is above 50%, yellow and red 
when the health is at 50% and 25% respectively) to increase the visibility of information about the player's health and base health.

With all of these comments and feedback, I can only hope that my game will keep improving as much as I am improving my game design skills and decisions.
