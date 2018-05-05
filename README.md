# platform-dinosaur-unity
A simple Google Chrome's dinosaur game replica in Unity

## Preview
![The Game](https://u.cubeupload.com/victorferraresi/dino.gif)

### How do I run the project?
1. Clone this repository to your local machine (`git clone https://github.com/VictorFerraresi/platform-dinosaur-unity.git`)
2. Download and Install the [Unity Engine Development Kit](https://unity3d.com/pt/get-unity/download)
3. Open the Unity Engine and load the folder you just cloned as a Unity Project
4. Click on the play button!
5. Game Commands: Spacebar = JUMP.

By default, if there are less than 4 base genomes in the Genomes directory, the game will let you play by yourself and record your inputs until there are 4 base generated genomes. After that, the game will start playing by itself, doing [Crossovers](#crossover-function) and [Mutations](#mutation-function) over your base Genomes, and calculating their Fitness using out [Fitness Function](#fitness-function).

### How does the genetic algorithm works?
By the moment you have 4 base genomes in the Genomes directory, the algorithm will generate another 12 Genomes doing [Crossovers](#crossover-function) and [Mutations](#mutation-function) and will commit them to the Genomes/CrossedOvers directory. By doing that, the game will play all the 12 new genomes by itself, calculating their fitness using the [Fitness Function](#fitness-function).

After calculating their fitness, the algorithm will select the top 4 fitness (by greater value) and delete all the others. We'll have, again, 4 base genomes to operate over again and again, iteratively, until our Objective Function is achieved.

#### Crossover function
It iterates over two genomes, cromossome by cromossome and randomically picks one of the 2 cromossomes and adds it to the new genome. Let's say we have Genome A as (X Y Z D E F) and Genome B as (H I J K L M). Their crossover result might me Genome C as (X Y J D L M).

#### Mutation function
It picks one single genome and adds a new random-controlled cromossome to it. It's random-controlled because it's values are random inside a range, that covers the average of the jumps distance of that entire genome plus a random factor between -1.0 and 0.0. Let's say we have a Genome A as (X Y Z D E F). It's mutation result might be Genome B (X Y Z D G E F).

#### Fitness function
It's based on the number of cactus that the dinosaur jumped without colliding. If the dinosaur jumped over 9 cactus and crashed on the 10th, that genome's fitness value will be 9.

### Why'd you want to do that?
I'm finishing my graduation in Computer Science by the half of this year and this replica is a part of my undergraduate thesis.

### Project objectives

My objective is to generate an JSON output of some scenarios meanwhile I play the game (speed, crash time, crash speed, distance when I jumped, etc) and write a genetic algorithm to generate the optimal output so the computer can play the game by itself.

The algorithms and the output will be commited to this same repository.

### Objectives Achieved
1. ![Done](http://u.cubeupload.com/victorferraresi/icodone.png) Creation of the Unity game replica
2. ![Done](http://u.cubeupload.com/victorferraresi/icodone.png) Generation of a JSON output of the played game scenario
3. ![Done](http://u.cubeupload.com/victorferraresi/icodone.png) Implementation of the genetic algorithm
4. ![WIP](http://u.cubeupload.com/victorferraresi/icowip.png) Enhancement of the Mutation function
