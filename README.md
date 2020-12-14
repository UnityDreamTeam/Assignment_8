# Assignment 8
This project is part of Game Development course. It shows a basic paving mechanism in Unity in order to build a simple 2D world.
This project is based on existing project from class. It uses BFS algorithm in order to help the user find a path
to the target when moving with mouse clicking.  
We added Dijkstra algorithm in order to find the shortest path to the destination, where the paves uses as weight.

## Dijkstra algorithm

![dijkstra-weights](https://user-images.githubusercontent.com/73671381/102028812-9a799900-3db4-11eb-9b4e-3ed866503576.gif)  

## Dijkstra algorithm VS BFS
Here you can see how the players is moving while using BFS algorithm vs while using Dijkstra algorithm.
You can notice that when we add weights, Dijkstra algorithm will pick a **shorter** path then BFS does.  
![dijkstra-vs-bfs](https://user-images.githubusercontent.com/73671381/102148411-0d4a4900-3e75-11eb-91fb-8eabedece1aa.gif)  
