using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatformScript : MonoBehaviour
{
    public float breakTimer;
    public bool breaking = false;
    public float resetTimer;

    public GameObject platformObject;

    private void Start()
    {
        breaking = false;
        platformObject.SetActive(true);
    }

    private IEnumerator BreakPlatform()
    {
        breaking = true;
        float timer = 0;

        while(timer < breakTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        breaking = false;
        platformObject.SetActive(false);

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
        yield return null;
    }
    public void OnCollision() 
    { 
        if(breaking == false && platformObject.activeInHierarchy)
        {
            StartCoroutine(BreakPlatform());
        }
    }
}
