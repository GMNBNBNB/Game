using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedBox;
    [SerializeField] GameObject openedBox;
    [SerializeField] bool opened;
   public override void Interact(Character character)
    {
        if(opened == false)
        {
            opened = true;
            closedBox.SetActive(false);
            openedBox.SetActive(true);
        }
    }
}
