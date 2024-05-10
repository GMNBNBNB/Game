using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    PlayerController2D playerController;
    Rigidbody2D Rigidbody;

    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractAera = 1.2f;

    Character Character;
    [SerializeReference] HighlightController HighlightController;
    private void Awake()
    {
        playerController = GetComponent<PlayerController2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Character = GetComponent<Character>();
    }
    private void Update()
    {
        Check();
        if ((Input.GetMouseButtonDown(1)))
        {
            Interact();
        }
    }

    private void Check()
    {
        Vector2 position = Rigidbody.position + playerController.lastmotionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractAera);
        
        foreach (Collider2D collider in colliders)
        {
            Interactable hit = collider.GetComponent<Interactable>();
            if (hit != null)
            {
                HighlightController.Highlight(hit.gameObject);
                return;
            }
        }

        HighlightController.Hide();
    }

    private void Interact()
    {
        Vector2 position = Rigidbody.position + playerController.lastmotionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractAera);
        foreach (Collider2D collider in colliders)
        {
            Interactable hit = collider.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(Character);
                break;
            }
        }
    }
}
