using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crops
{

}

public class CropsManager : MonoBehaviour
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTileMap;

    Dictionary<Vector2Int, Crops> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, Crops>();
    }

    public bool Check(Vector3Int pos)
    {
        return crops.ContainsKey((Vector2Int)pos);
    }

    public void Plow(Vector3Int pos)
    {
        if(crops.ContainsKey((Vector2Int)pos))
        {
            return;
        }

        CreatePlowedTile(pos);
    }

    public void Seed(Vector3Int pos)
    {
        targetTileMap.SetTile(pos,seeded);
    }

    private void CreatePlowedTile(Vector3Int pos)
    {
        Crops crop = new Crops();
        crops.Add((Vector2Int)pos, crop);

        targetTileMap.SetTile((Vector3Int)pos, plowed);
    }
}
