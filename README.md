# Assignment 8
This project is part of Game Development course and based on existing project from class. It shows a basic paving mechanism in Unity in order to build a simple 2D world. It uses BFS algorithm in order to help the user find a path to the target when moving with mouse clicking.  
We added **Dijkstra** algorithm in order to find the shortest path to the destination, where the paves uses as weights.

## Dijkstra Algorithm
This is an example for how Dijkstra algorithm works.  
![dijkstra-weights](https://user-images.githubusercontent.com/73671381/102028812-9a799900-3db4-11eb-9b4e-3ed866503576.gif)  

## Dijkstra VS BFS
Here you can see how the players is moving while using BFS algorithm vs while using Dijkstra algorithm.
You can notice that when we add weights, Dijkstra algorithm will pick a **shorter** path then BFS does.  
![dijkstra-vs-bfs](https://user-images.githubusercontent.com/73671381/102148411-0d4a4900-3e75-11eb-91fb-8eabedece1aa.gif)  

## Speed And Algorithms
Here you can see how the player speed affected when it walks on the different tiles. Each tile has a different weight
and thus Dijkstra algorithm can pick the **fastest** path in contrast to BFS algorithm. You can easly see in the following video
which algorithm gets the player faster to the destination.  
![speed](https://user-images.githubusercontent.com/73671381/102149223-9dd55900-3e76-11eb-8b66-b2bc6d8676ee.gif)  

## Code & Design
In order to create easy to extend code and preserve abstraction, we created an additional interface:  
```
public interface IPathFinder<NodeType>
{
    List<NodeType> GetPath(IGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations = 1000);
}
...
List<Vector3Int> shortestPath = pathFinder.GetPath(tilemapGraph, startNode, endNode, maxIterations);
```
This interface uses as a arbitrator in code to pick path finder algorithm at run time. We don't need to know which algorithm 
is used in order to run it. :smile:  
If we want to add a third algorithm, for example A*, all we have to do is to implement the interface (and of course the algorithm) and the rest of the code will take care the rest. :wink:  
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
