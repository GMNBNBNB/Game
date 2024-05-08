using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class ToolController : MonoBehaviour
{
    PlayerController2D Character;
    Rigidbody2D Rigidbody;

    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractAera = 1.2f;

    private void Awake()
    {
        Character = GetComponent<PlayerController2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0)))
        {
            UseTool();
        }
    }

    private void UseTool()
    {
        Vector2 position = Rigidbody.position + Character.lastmotionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position,sizeOfInteractAera);
        foreach(Collider2D collider in colliders)
        {
            ToolHit hit = collider.GetComponent<ToolHit>();
            if(hit != null)
            {
                hit.Hit();
                break;
            }
        }
    }
}
