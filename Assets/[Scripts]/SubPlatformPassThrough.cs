using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPlatformPassThrough : MonoBehaviour
{
    public BreakablePlatformScript parentScript;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        parentScript.OnCollision();
    }
}
