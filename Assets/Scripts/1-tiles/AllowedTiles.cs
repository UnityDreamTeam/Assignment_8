using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedTiles : MonoBehaviour  {
    [SerializeField] TileBase[] allowedTiles = null;
    [SerializeField] int[] weights = null; //Weight for each different tile (how slow movement is)
    static Dictionary<TileBase, int> map;

    private void Start()
    {
        map = new Dictionary<TileBase, int>();
        for (int i = 0; i < weights.Length; i++)
        {
            map.Add(allowedTiles[i], weights[i]);
        }
    }
    public bool Contain(TileBase tile) {
        return allowedTiles.Contains(tile);
    }

    public static int GetWeight(TileBase tile)
    {
        return map[tile];
    }

    public TileBase[] Get() { return allowedTiles;  }
}
