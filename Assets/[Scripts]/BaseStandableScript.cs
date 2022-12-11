using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStandableScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.parent != transform && !collision.gameObject.CompareTag("Ground"))
            collision.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(transform.parent != collision.transform.parent && !collision.gameObject.CompareTag("Ground"))
            collision.transform.parent = null;
    }
}
