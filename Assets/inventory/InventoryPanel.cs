using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] List<InventoryButton> buttons;

    private void Start()
    {
        SetIndex();
        Show();
    }

    private void OnEnable()
    {
        Show();
    }

    private void SetIndex()
    {
        for (int i = 0; i < inventory.slot.Count && i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    public void Show()
    {
        for (int i = 0; i < inventory.slot.Count && i < buttons.Count; i++)
        {
            if (inventory.slot[i].item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(inventory.slot[i]);
            }
        }
    }
}
