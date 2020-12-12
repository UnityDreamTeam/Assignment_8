using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dijkstra
{
	private static void ConstructPath(List<Node> distances, Vector3Int startNode, Node end_node,  List<Vector3Int> outputPath)
    {
		//Construct the path at the end
		while (!end_node.Position.Equals(startNode))
		{
			outputPath.Add(end_node.Position);
			//Jump to the father
			Vector3Int father = end_node.Father;
			for (int j = 0; j < distances.Count; j++)
			{
				if (distances[j].Position.Equals(father))
				{
					end_node = distances[j];
					break;
				}
			}
		}

		outputPath.Add(startNode);//Add the source node also
		outputPath.Reverse();
	}
	private static void FindPath(
            IGraph<Vector3Int> graph,
			Vector3Int startNode, Vector3Int endNode,
            List<Vector3Int> outputPath, int maxiterations = 1000)
    {
		//Used to insert new nodes into the dynamic list of vertexes
		HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>();

		//List of dynamicly generated nodes with their distances
		List<Node> nodes = new List<Node>();

		//Priority Queue to remove every iteration the neighbor with smallest weight
		List<Node> pq = new List<Node>();

		//Initialize the current node to be starting node. it has no father and distance to itself is zero
		Node currentNode = new Node(startNode, 0, new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue));
		//Add the first node to the list of existing nodes.
		nodes.Add(currentNode);

		for (int k = 0; k < maxiterations; ++k)
		{
			//Add new neighbors if we find new vertexes
			foreach (var neighbor in graph.Neighbors(currentNode.Position))
			{
				if (!closedSet.Contains(neighbor))
				{
					Node newNode = new Node(neighbor, int.MaxValue, new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue));
					nodes.Add(newNode);
					pq.Add(newNode);

					closedSet.Add(neighbor);
				}
			}

			//Now we can run the dijkstra algrithm
			foreach (var edge in graph.Edges(currentNode.Position))
			{
				//Find the other node
				Node otherNode = null;
				for (int j = 0; j < nodes.Count; j++)
				{
					if (edge.Other.Equals(nodes[j].Position))
					{
						otherNode = nodes[j];
						break;
					}
				}

				//Update father, if we found a new one
				if (currentNode.Distnace + edge.Weight < otherNode.Distnace)
				{
					otherNode.Father = currentNode.Position;
				}

				//Update lowest distnace
				otherNode.Distnace = Mathf.Min(currentNode.Distnace + edge.Weight, otherNode.Distnace);

				var item = pq.SingleOrDefault(node => node.Position.Equals(otherNode.Position));
				if(item != null)
                {
					pq.Remove(item);
					pq.Add(new Node(otherNode));
				}
			}

			//Find node with minimum distance and remove it, if list isn't empty
			if (pq.Any())
			{
				currentNode = pq.Aggregate((node1, node2) => node1.Distnace < node2.Distnace ? node1 : node2);
				pq.Remove(currentNode);
			}

            if (currentNode.Position.Equals(endNode))
            {
				//We found the target node, now construct the path
				ConstructPath(nodes, startNode, currentNode, outputPath);
				break;
            }
		}
	}

    public static List<Vector3Int> GetPath(IGraph<Vector3Int> graph, Vector3Int startNode, Vector3Int endNode, int maxiterations = 1000)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        FindPath(graph, startNode, endNode, path, maxiterations);
        return path;
    }
}