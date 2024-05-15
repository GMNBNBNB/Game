using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject toolbarPanel;
    private void Start()
    {
        panel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            panel.SetActive(!panel.activeInHierarchy);
            toolbarPanel.SetActive(!toolbarPanel.activeInHierarchy);
        }
    }
}
