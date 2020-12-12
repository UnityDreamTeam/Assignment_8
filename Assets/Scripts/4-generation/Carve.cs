using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Carve : MonoBehaviour
{
    [Tooltip("Button to carve")]
    [SerializeField] KeyCode carveKey;

    [Tooltip("The tile change after carve")]
    [SerializeField] TileBase changeTile;

    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    [Tooltip("timeToCarve")]
    [SerializeField] float time = 0.8f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("X"))
        {
            carveTil();
        }  
    }


    /**
     * method that carve Tile 
     **/
    void carveTil()
    {
        Vector3 targetTile = transform.position;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetTile = transform.position + Vector3.up;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetTile = transform.position + Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetTile = transform.position + Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetTile = transform.position + Vector3.right;
        }

        StartCoroutine(carveCoroutine(targetTile)); 
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
        yield return new WaitForSeconds(time);
        checkTile(targetTile);
    }
}
