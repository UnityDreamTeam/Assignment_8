using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Carve : MonoBehaviour
{
    [Tooltip("The tile change after carve")]
    [SerializeField] TileBase changeTile;

    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    [Tooltip("Time to carve")]
    [SerializeField] float timeToCarve = 0.8f;


    private Vector3[] vectors = { Vector3.up, Vector3.down, Vector3.left, Vector3.right};
    private readonly int notPressed = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        carveTil();
    }

    /**
     * method that carve Tile 
     */
    void carveTil()
    {
        if (Input.GetButton("X"))
        { 
            if (getIndexByMove() != notPressed)
            {
                StartCoroutine(carveCoroutine(transform.position + vectors[getIndexByMove()]));
            }
        }

        if (getIndexByMove() != notPressed)
        {
            if (Input.GetButton("X"))
            {
                StartCoroutine(carveCoroutine(transform.position + vectors[getIndexByMove()]));
            }
        }
    }

    private int getIndexByMove()
    {
        if (Input.GetButton("UP"))
        {
            return 0;
        }

        else if (Input.GetButton("DOWN"))
        {
            return 1;
        }

        else if (Input.GetButton("LEFT"))
        {
            return 2;
        }

        else if (Input.GetButton("RIGHT"))
        {
            return 3;
        }

        return notPressed;
    }


    /**
     * check and change the tile
     **/
    private void checkTile(Vector3 position)
    {
        TileBase tileOnNewPosition = TileOnPosition(position);
        if (!allowedTiles.Contain(tileOnNewPosition))
        {
            tilemap.SetTile(tilemap.WorldToCell(position), changeTile);
        }
    }

    /**
     * return which tile is in the position
     **/
    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    IEnumerator carveCoroutine(Vector3 targetTile)
    {
        yield return new WaitForSeconds(timeToCarve);
        checkTile(targetTile);
    }
}
