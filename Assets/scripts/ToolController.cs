using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;

public class ToolController : MonoBehaviour
{
    PlayerController2D Character;
    Rigidbody2D Rigidbody;
    Animator animator;
    ToolBarController toolBarController;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractAera = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileReadController tileReadcontroller;
    [SerializeField] float maxDistance = 1.5f;
    
    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        Character = GetComponent<PlayerController2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        toolBarController = GetComponent<ToolBarController>();
        animator = GetComponent<Animator>();
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

        Item item = toolBarController.GetItem;
        if(item == null) {  return false; }
        if(item.onAction == null) { return false; }

        animator.SetTrigger("act");
        bool complete = item.onAction.OnApply(position);

        if (complete == true)
        {
            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventoryContainer);
            }
        }

        return complete;
    }

    private void UseToolGrid()
    {
        if(selectable == true)
        {
            Item item = toolBarController.GetItem;
            if (item == null) { return; }
            if (item.onTileMapAction == null) { return; }

            animator.SetTrigger("act");
            bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition,tileReadcontroller);

            if (complete == true)
            {
                if (item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item,GameManager.Instance.inventoryContainer);
                }
            }
        }
    }
}
