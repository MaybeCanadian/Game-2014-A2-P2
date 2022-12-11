using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorPlatformScript : MonoBehaviour
{
    public float verticalRange = 8.0f;
    public float timerToRise = 3.0f;
    public bool onlyPlayer = true;

    public bool move = false;

    public float localTimer = 0.0f;

    private Vector2 startPosition;
    private Vector2 endPosition;
    public List<GameObject> thingsOnPlatform;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector2(startPosition.x, startPosition.y + verticalRange);
        thingsOnPlatform = new List<GameObject>();
        localTimer = 0.0f;
    }

    private void FixedUpdate()
    {
        if(move == true)
        {
            localTimer += Time.fixedDeltaTime * 1.0f/timerToRise;
        }
        else
        {
            localTimer -= Time.fixedDeltaTime * 1.0f/timerToRise;
        }

        localTimer = Mathf.Clamp(localTimer, 0.0f, 1.0f);

        transform.position = Vector2.Lerp(startPosition, endPosition, localTimer);
    }

    private bool CheckForPlayer()
    {
        foreach(GameObject go in thingsOnPlatform)
        {
            if(go.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        thingsOnPlatform.Add(collision.gameObject);
        collision.transform.SetParent(transform);

        move = (onlyPlayer) ? CheckForPlayer() : true;   
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        thingsOnPlatform.Remove(collision.gameObject);
        collision.transform.SetParent(null);

        move = (onlyPlayer) ? CheckForPlayer() : ((thingsOnPlatform.Count > 0) ? true : false); //if we check we check otherise if we have something we good
    }
}
