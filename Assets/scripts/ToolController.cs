using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector2 = UnityEngine.Vector2;

public class ToolController : MonoBehaviour
{
    PlayerController2D Character;
    Rigidbody2D Rigidbody;

    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractAera = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileReadController tileReadcontroller;
    [SerializeField] float maxDistance = 1.5f;
    [SerializeField] CropsManager cropsManager;
    [SerializeField] TileData plowableTiles;
    
    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        Character = GetComponent<PlayerController2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();
        if ((Input.GetMouseButtonDown(0)))
        {
            if (UseTool()) { return; }
            UseToolGrid();
        }
    }

    private void SelectTile()
    {
        selectedTilePosition = tileReadcontroller.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 characterPositionn = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPositionn, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }

    private bool UseTool()
    {
        Vector2 position = Rigidbody.position + Character.lastmotionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position,sizeOfInteractAera);
        foreach(Collider2D collider in colliders)
        {
            ToolHit hit = collider.GetComponent<ToolHit>();
            if(hit != null)
            {
                hit.Hit();
                return true;
            }
        }

        return false;
    }

    private void UseToolGrid()
    {
        if(selectable == true)
        {
            TileBase tileBase = tileReadcontroller.GetTileBase(selectedTilePosition);
            TileData tileData = tileReadcontroller.GetTileData(tileBase);
            if(tileData != plowableTiles) { return; }

            if (cropsManager.Check(selectedTilePosition))
            {
                cropsManager.Seed(selectedTilePosition);
            }
            else
            {
                cropsManager.Plow(selectedTilePosition);
            }
        }
    }
}
