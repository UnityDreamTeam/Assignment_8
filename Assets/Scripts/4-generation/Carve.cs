using System;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("X"))
        {
            carveTilA();
        }

        carveTilB();
    }

    private void carveTilB()
    {
        Vector3 targetTile = transform.position;
        if (Input.GetButton("UP"))
        {
            if (Input.GetButton("X"))
            {
                targetTile = transform.position + Vector3.up;
                StartCoroutine(carveCoroutine(targetTile));
            }
        }

        if (Input.GetButton("DOWN"))
        {
            if (Input.GetButton("X"))
            {
                targetTile = transform.position + Vector3.down;
                StartCoroutine(carveCoroutine(targetTile));
            }
        }

        if (Input.GetButton("LEFT"))
        {
            if (Input.GetButton("X"))
            {
                targetTile = transform.position + Vector3.left;
                StartCoroutine(carveCoroutine(targetTile));
            }
        }

        if (Input.GetButton("RIGHT"))
        {
            if (Input.GetButton("X"))
            {
                targetTile = transform.position + Vector3.right;
                StartCoroutine(carveCoroutine(targetTile));
            }
        }
    }


    /**
     * method that carve Tile 
     **/
    void carveTilA()
    {
        Vector3 targetTile = transform.position;

        if (Input.GetButton("UP"))
        {
            targetTile = transform.position + Vector3.up;
        }

        if (Input.GetButton("DOWN"))
        {
            targetTile = transform.position + Vector3.down;
        }

        if (Input.GetButton("LEFT"))
        {
            targetTile = transform.position + Vector3.left;
        }

        if (Input.GetButton("RIGHT"))
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
        yield return new WaitForSeconds(timeToCarve);
        checkTile(targetTile);
    }
}
