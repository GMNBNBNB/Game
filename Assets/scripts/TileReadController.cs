using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileReadController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] List<TileData> tileDatas;
    Dictionary<TileBase, TileData> dataFromTiles;

    private void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
         
        foreach(TileData tileData in tileDatas)
        {
            foreach(TileBase tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);  
            }
        }
    }
    public Vector3Int GetGridPosition(Vector2 pos, bool mousePosition)
    {
        Vector3 worldPosition;

        if(mousePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(pos);
        }
        else
        {
            worldPosition = pos;
        }

        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        return gridPosition;
    }

    public TileBase GetTileBase(Vector3Int gridPosition)
    {

        TileBase tile = tilemap.GetTile(gridPosition);

        //Debug.Log("Tile in position =" + gridPosition + "is" + tile);

        return tile;
    }

    public TileData GetTileData(TileBase tileBase)
    {

        return dataFromTiles[tileBase];
    }
}
