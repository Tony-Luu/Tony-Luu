# Issues with the Dash ability
## The problem
The problems were: The dash ability gave the character far too much force in a direction, it allowed the player to dash multiple times in the air, and also allowed the player to 
dash through anything.

## The solution
The force of the dash ability was toned down a lot with the force mode set to Impulse. Also, I implemented a counter to check the amount of times the player has dashed to prevent
multiple dashes in the air and changed the layer of the player to only allow the player to dash through certain obstacles.



# Platform Issues
## The problem
The player was not moving with the platform so the player ends up slipping off the platform while standing on it.

## The solution
Setting the platform's rigidbody to Kinematic and setting the player's rigidbody equal to the platform's rigidbody.
