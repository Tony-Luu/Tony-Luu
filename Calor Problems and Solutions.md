# Pushing and pulling objects

## The Problem
The character can push and pull objects off the edge of platforms and be hanging onto the object while moving despite being off the ground.

## The Solution
I created linecasts on the character and the grabbed objects so that when the character is grabbing onto an object, the liecasts detects whenever or not the character or the object
is no longer touching the platform. If the character or grabbed object is not touching the edge of the platform, disable the character's movement.



# Animation Issues

## The Problem
The character would sometimes flicker between animation states such as jumping, falling, landing, and preparing to jump.

## The Solution
My solution was to implement a Finite State Machine that manages all of the conditions of the state changes. I used enumeration to keep track of all of the animation states to make
it work with the animator as I used an integer for state transitions. One of the methods for fixing the transitions for jumping and falling was to use the character's velocity to 
trigger the jumping and falling. For jumping, the player's velocity must be above 0 to trigger the jumping state and for falling, the player's velocity must be below 0 to trigger 
the falling state.
