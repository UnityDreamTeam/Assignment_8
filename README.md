# Assignment 8
This project is part of Game Development course. It shows a basic paving mechanism in Unity in order to build a simple 2D world.
This project is based on existing project from class. It uses BFS algorithm in order to help the user find a path
to the target when moving with mouse clicking.  
We added Dijkstra algorithm in order to find the shortest path to the destination, where the paves uses as weight.

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
