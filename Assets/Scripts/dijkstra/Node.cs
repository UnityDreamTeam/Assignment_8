using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Vector3Int position;
    private int distnace;
    private Vector3Int father;//Father vertex, in order to reconstruct the path

    public Node(Node other) : this(other.Position, other.Distnace, other.Father) { }
    public Node(Vector3Int position, int distance, Vector3Int father)
    {
        this.Position = position;
        this.Distnace = distance;
        this.Father = father; //No father
    }

    public int Distnace { get => distnace; set => distnace = value; }
    public Vector3Int Position { get => position; set => position = value; }
    public Vector3Int Father { get => father; set => father = value; }
}
