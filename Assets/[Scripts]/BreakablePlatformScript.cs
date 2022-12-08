using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatformScript : MonoBehaviour
{
    public float breakTimer;
    public bool breaking = false;
    public float resetTimer;

    public GameObject platformObject;
    public BoxCollider2D boxCol;

    public Animator anims;

    private void Start()
    {
        breaking = false;
        boxCol = GetComponent<BoxCollider2D>();
        anims = GetComponent<Animator>();
        platformObject.SetActive(true);
    }

    private IEnumerator BreakPlatform()
    {
        breaking = true;
        anims.SetBool("Breaking", true);
        float timer = 0;

        while(timer < breakTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        breaking = false;
        anims.SetBool("Breaking", false);
        platformObject.SetActive(false);
        boxCol.enabled = false;

        StartCoroutine(ResetPlatform());
        yield return null;
    }

    private IEnumerator ResetPlatform()
    {
        float timer = 0;

        while(timer < resetTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        breaking = false;
        platformObject.SetActive(true);
        boxCol.enabled = true;
        yield return null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (breaking == false && platformObject.activeInHierarchy)
        {
            StartCoroutine(BreakPlatform());
        }
    } 
    
}
