using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool action/Seed Tile")]

public class seedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileReadController tileReadController)
    {

        if (tileReadController.cropsManager.Check(gridPosition) == false)
        {
            return false;
        }

        tileReadController.cropsManager.Seed(gridPosition);

        return true;
    }

    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.Remove(usedItem);
    }

}
