# Assignment 8
This project is part of Game Development course and based on existing project from class. It shows a basic paving mechanism in Unity in order to build a simple 2D world. It uses BFS algorithm in order to help the user find a path to the target when moving with mouse clicking.  
We added **Dijkstra** algorithm in order to find the shortest path to the destination, where the paves uses as weights.

## Dijkstra Algorithm
This is an example for how Dijkstra algorithm works.  
![dijkstra-weights](https://user-images.githubusercontent.com/73671381/102279407-fdde0500-3f33-11eb-9cdf-22426f6f673e.gif)  

## Dijkstra VS BFS
Here you can see how the players is moving while using BFS algorithm vs while using Dijkstra algorithm.
You can notice that when we add weights, Dijkstra algorithm will pick a **shorter** path then BFS does.  
![dijkstra-vs-bfs](https://user-images.githubusercontent.com/73671381/102279390-f9195100-3f33-11eb-842c-5655fc277cb1.gif)  

## Speed And Algorithms
Here you can see how the player speed affected when it walks on the different tiles. Each tile has a different weight
and thus Dijkstra algorithm can pick the **fastest** path in contrast to BFS algorithm. You can easly see in the following video
which algorithm gets the player faster to the destination.  
![speed](https://user-images.githubusercontent.com/73671381/102279415-01718c00-3f34-11eb-9eab-ffda80c5eadc.gif)  

## Code & Design
In order to create easy to extend code and preserve abstraction, we created an additional interface:  
```
public interface IPathFinder<NodeType>
{
    List<NodeType> GetPath(IGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations = 1000);
}
```
```
//TargetMover.cs
...
List<Vector3Int> shortestPath = pathFinder.GetPath(tilemapGraph, startNode, endNode, maxIterations);
```
This interface uses as an arbitrator in code to pick path finder algorithm at run time. We don't need to know which algorithm 
is used in order to run it. :smile:  
If we want to add a third algorithm, for example A*, all we have to do is to implement the interface (and of course the algorithm) and the code will take care the rest. :wink:  
We also added the ability for the user to choose the algorithm via Unity editor while playing, and we used a callback mechanism
instead of polling the attributes in while true loop:
```
private void OnValidate()
    {
        if (useDijkstra)
        {
            pathFinder = new Dijkstra<Vector3Int>();
        }
        else
        {
            pathFinder = new BFS<Vector3Int>();
        }
    }
```
This function will be called as a callback, once we made a change on unity editor.


## Carving
The player carves a tile in order to move forward and pave new ways.  
![ezgif com-gif-maker](https://user-images.githubusercontent.com/57867818/102248422-d3775200-3f09-11eb-98d1-751d83e65fe7.gif)

## Map Generator

In this section we have improved the map generator/cave generator of the original project mentioned above.
Originally, the method produced a cave-like map built from two kinds of tiles. Our improvement is to add 2 more tiles into the map, and by that adding additional diversity and interest to the game.
The tiles will spread around the map and gradually come closer toward similar tiles which will make them together to look like an area.  
![CaveGeneratorGIFCloseup](https://user-images.githubusercontent.com/44766214/102263614-39210980-3f1d-11eb-8414-d2cd49dc50ac.gif)

