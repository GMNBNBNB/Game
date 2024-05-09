using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemContainer))]
public class ItemContainerEdior : Editor
{
    public override void OnInspectorGUI()
    {
        ItemContainer container = target as ItemContainer;
        if (GUILayout.Button("Clear container"))
        {
            for (int i = 0; i < container.slot.Count; i++)
            {
                container.slot[i].item = null;
                container.slot[i].count = 0;
            }
        }
        DrawDefaultInspector();
    }
}
