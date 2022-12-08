using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorPlatformScript : MonoBehaviour
{
    public float verticalRange = 8.0f;
    public float verticalSpeed = 3.0f;
    public bool onlyPlayer = true;

    public bool move = false;

    private Vector2 startPosition;
    private List<GameObject> thingsOnPlatform;

    private void Start()
    {
        startPosition = transform.position;
        thingsOnPlatform = new List<GameObject>();
    }

    private void Update()
    {
        if (move)
        {
            transform.position = new Vector2(startPosition.x,
            Mathf.PingPong(verticalSpeed * Time.time, verticalRange) + startPosition.y);
        }
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
        thingsOnPlatform.Add(collision.gameObject);
        collision.transform.SetParent(null);

        move = (onlyPlayer) ? CheckForPlayer() : ((thingsOnPlatform.Count > 0) ? true : false); //if we check we check otherise if we have something we good
    }
}
