This prototype is a technical demonstration that displays procedural generation of the level map and the various puzzle events such as the Spike Trap and the Sokoban. 
My responsibilities in this team project was to create procedurally generated puzzle events for replayability and combat and movement for the playable characters. 


# Adapting a maze algorithm

## The Problem

The algorithm I have used to generate the maze is very linear which offers only one pathway towards the exit. The Depth-First Search algorithm is what I used for generating 
the maze by randomly selecting a direction and destroying the wall in that direction.

## The Solution
I had to define what a dead-end is in the maze algorithm in order to remove the dead-ends. When it becomes generated, the maze offers multiple pathways towards the exit.

## Detailed Explanation
I have learned the Depth First Search algorithm to procedurally create a maze and altered it to create a braid maze (which is a type of maze that removes most of the dead-ends).
I have also learned how to procedurally create a level for Sokoban by creating a level with walls around the area as a border and then separating the area within the walls like
a 3x3 grid. Within the 3x3 grid, a template is randomly selected and generated for example, a 3x3 template containing a wall in the middle.

This problem I am mentioning came across as more of a design issue with the maze rather than a coding one. The maze implemented for the project was used for an event where the 
player must traverse through the maze filled with spikes under a time limit. However, the algorithm used to implement the maze always produced just one pathway towards the exit
which made it very linear to complete. My friend whom I am working with wanted a maze with more than one way to traverse through the maze. The algorithm I used to procedurally 
generate the maze is called the Depth First Search. I filled the entire space in the scene with walls and then the algorithm hollows out the maze by randomly selecting a 
direction to take such as: Up, Down, Left or Right. Once the direction was chosen, it destroys 2 walls in the chosen direction. When a chosen direction detects that there are 
no more walls to destroy then it loops again until it cannot find any more walls to destroy.

At first, I adjusted the maze algorithm to randomly delete walls once the algorithm randomly chose a direction to destroy walls. The result of that change was very inconsistent
because the maze produced was either with lots of holes in the walls or it provided a more easier way to traverse out of the maze around the edges. It also did not look 
aesthetically pleasing nor did it look like a maze at all. So I recently asked for help from a lecturer who previously taught me and lead me to this link about all the types of
maze algorithms: http://www.astrolog.org/labyrnth/algrithm.htm.

He told me how a braid maze would suit the requirement my friend was asking for. Another problem I encountered was defining what a dead-end is in the algorithm and trying to 
remove all of the dead-ends in the maze. I have attempted to keep track of all of the clear pathways the algorithm has created with a Vector3 list and looped through the list to
check any adjacent walls. Unfortunately when I done that, the game suddenly froze. So I had to think of another way of checking dead-ends when the algorithm generates a maze and
I recently discovered how to do so within the algorithm rather than checking the dead-ends outside of it.

A dead-end in a maze is defined as a path that does not lead to the exit. When the algorithm randomly selects a direction but there are no walls to destroy, it checks that 
position to see if there are three neighbouring walls around it. If there is three neighbouring walls around that position then destroy that wall depending on the direction.
For example, if the up direction has randomly selected but if there is no wall ahead then it checks the adjacent positions (which are left, top and right) in the 2D gameobject
array. If there are adjacent walls then destroy the wall in the up direction. The algorithm will remove the majority of dead-ends in the maze depending on the direction the 
algorithm takes. This screenshot is part of a switch case statement I have used with an integer variable randomly having the values from 1 to 4 in a for loop.

![Screenshot_2016-10-23-17-03-36](https://user-images.githubusercontent.com/10073398/113450676-4395df00-93f8-11eb-80df-24320f549a35.png)

This is the end result. The assets and UI in this screenshot are merely placeholders and will change in the later builds.

 

# Issues with Serialization
## The Problem
I had to figure out how to store the procedurally generated node map whenever the player returns to the level map once they have exited the game.

## The Solution
I had to serialize the classes that would store the procedurally generated data in a saved file such as type of nodes, their positions, and the paths that connected the nodes.
I also had to ensure that the level manager would not procedurally regenerate a new node map once the player loads the node map from the previously saved file.

## Detailed Explanation
I initially had the problem with serializing the node gameobjects because it is procedurally generated so I thought of containing data that can be serialized instead. 
Since the level map is made out of nodes in a 2D gameobject array, I made a serializable class with variables that can hold the column and row positions of the nodes from the 2D
gameobject array.

The 2D int array contains the value that corresponds with the type of node it contained from the 2D gameobject array. The index for the values would pinpoint what type of node 
it is in the 2D gameobject array for example: Column 2 and Row 3 would be [2, 3] which contains a Story node which is 2 and 3 on the 2D int array just like the 2D gameobject 
array would have. Within [2, 3] would have the value of 2. The values within the 2D int array will correspond with the type of node for example: 0 = Start, 1 = Danger, 
2 = Story, 3 = Social, 4 = Real_Danger, 5 = Safe, 6 = Mystery, 7 = End. When the progress is saved into a file and then loaded up again, I had to prevent the script from 
procedurally regenerating the map again so I made a global gameobject with a script that holds a boolean that sets it to true and not destroy itself when the player continues.

So when the boolean is set to true, re-create the gameobject array with re-initializing it and looping through 2 for loops to check if the column and row values match the 
corresponding values to the type of node it is. For example, a Social node has the value of 3 and when it is looped through the 2D int array, column 2 and row 1 would have the 
number 3. The Social node gets recreated and inserted into the 2D gameobject array again with the values 2 and 1 to spawn with the X and Z positions respectively while Y is at 0.

I have made a function for saving node properties and made a check at the very beginning of the function to check if the previous node properties data is still there because 
every procedurally generated level map will have different amount of nodes. Another obstacle I have encountered while loading serialized data is to load the node data into their
correct positions. I previously had the node properties from the 2D gameobject array all saved into one file however, I discovered that there was no way to differentiate which 
node data belongs to which node. So I decided to serialize each individual node data from the 2D gameobject array. The SaveProperties function saves all of the nodes within
a for loop to check the gameobject array which node exists in what index with an if statement. If the node exists, loop through the integer list from the serialized class from
the node property script and insert the values based on the type of node it is for example: 0 = Start, 1 = Danger, 2 = Story, 3 = Social, 4 = Real_Danger, 5 = Safe, 6 = Mystery,
7 = End. Afterwards, it get serialized based on the directory of the saved game and its' name.

When the node properties are serialized properly, I have noticed yet another problem. I needed to find a way to recreate the node connections in the node array after loading all
of the nodes. The solution was to loop through the array and obtain the script component from the parent node to read the int list. Then used 2 for loops, one starts with 
x = col - 1 and the other as z = row - 1 and both increment until it reaches col and row + 2 respectively.

However, this solution was also causing a very annoying problem as well. At the very last column, there were 2 nodes that connected to each other despite the fact that the 
integer list it contained has no value that indicated that they were supposed to be connected to each other. I have discovered that the continue keyword within that if statement
that checks outside the boundaries of the node array must be the problem. But that was also the reason why it works so well until at the last column. I have thought of all kinds
of methods to solve this problem such as checking the adjacent nodes without the for loops and with if statements until I have finally figured out the workaround.

Instead of trying to fix the loop that unnecessarily added those adjacent nodes together when the for loops hit the boundaries of the node array, I filter the gameobject list 
that handles the connections. This function is in the script called Node_Properties which holds all of the information about that particular node such as the sprite it has,
the type of node it is and what node connects to itself. The final part of fixing all of this is to recreate the state of the nodes such as if their connected parts have been 
revealed. I have made for loops to recreate the connections and render any paths that were shown in the previous playthrough.

These for loops are added within a function called LoadProperties after the node data has been deserialized. The for loops check if the node array in the column and row indexes
are not null. If they are not then recreate the connections. Afterwards, filter through the list from the node properties script and render and reload the paths again. If a path
has been revealed before then reveal the connections and render the node sprite again.
