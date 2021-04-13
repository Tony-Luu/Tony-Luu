# Revamping the NPC's sight detection

## The Problem
Replacing the grid-based pathfinding algorithm with the NavMesh Agent meant that I had to discover another way for the NPC to detect and chase the player.

## The Solution
Calculate the angle between the player and the NPC by using a Vector3 variable to calculate the direction between the NPC and PC.

## Detailed Explanation
The layout of the level used to be locked into a grid because it made use of an A* pathfinding algorithm that checked which square was empty in the scene. It was used for the NPC's
movement and finding the player character. I replaced it with the NavMesh Agent component in Unity but it introduced a lot of problems when I made this transition. The game ends 
when the player reaches the arrows on the bottom of the floor and shows the stats.

I have decided to revisit an old project of mine that I have made for my dissertation and update it because I wanted to use the NavMesh Agent component Unity has for path finding.
However, upon changing the way the characters move around the level, it introduced new problems. At first, I was trying to create a cone shaped mesh and a collider with it as the
NPC's field of vision but unfortunately, it was beyond what I knew because it required maths. Another method I have tried to implement was a semi-circle mesh as the field of vision
but when I attached a mesh collider to it, the shape of the collider was an oblong and not a semi-circle. So I researched another way to accurately shape the field of vision. 
I have stumbled upon using multiple raycasts as the NPC's field of vision to shape it like a cone but the biggest problem is that it will take up a lot of resources because there
will be multiple raycasts coming from multiple NPC's which will lag the game. I continued to search for a solution until I have discovered one that finally worked. I have used a 
Vector3 variable to calculate the direction between the NPC and PC and then use a float variable to store the calculated angle from Vector3.Angle.


# Raycast Detection Issue

## The Problem
The raycast that detected around corners made the NPC obstruct itself around the corners of the walls.

## The Solution
Replacing the thin raycast with a long, thick box collider as a means to detect the player prevented the NPC from obstructing itself.

## Detailed Expalnation
One of the problems I have encountered is that using a raycast to detect around corners when the player is detected, only made the NPC shoot at the corners of the wall and not the
player because it was too precise. The solution to this was to create a thin box collider with the width of the NPC's projectile to detect the player so that once the NPC detects
the player, the NPC can navigate around corners and ensure that the projectile does hit the player and not the corners of a wall. It also follows the player until it collides with
the payer and in one of my methods, I have an if statement that checks of the angle is at a certain value then activate the code within the if statement.
