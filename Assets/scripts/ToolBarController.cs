using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 10;
    int selectedTool;

    public Action<int> onChange;

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            if(delta > 0)
            {
                selectedTool ++;
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            else
            {
                selectedTool --;
                selectedTool = (selectedTool < 0 ? toolbarSize - 1 : selectedTool);
            }
            onChange?.Invoke(selectedTool);
        }
    }

    internal void Set(int id)
    {
        selectedTool = id;
    }
}
