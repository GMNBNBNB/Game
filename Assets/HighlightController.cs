using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    [SerializeField] GameObject highelighter;

    GameObject currentTarget;
    public void Highlight(GameObject target)
    {
        if(currentTarget == target)
        {
            return;
        }
        Vector3 targetPos = target.transform.position;
        Highlight(targetPos);
    }
    public void Highlight(Vector3 targetPos)
    {
        highelighter.SetActive(true);
        highelighter.transform.position = targetPos;
    }
    public void Hide()
    {
        currentTarget = null;
        highelighter.SetActive(false);
    }
}
