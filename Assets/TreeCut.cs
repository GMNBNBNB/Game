using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCut : ToolHit
{
    public override void Hit()
    {
        Destroy(gameObject);
    }
}
