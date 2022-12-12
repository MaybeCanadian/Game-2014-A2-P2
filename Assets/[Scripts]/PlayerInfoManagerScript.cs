using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManagerScript : MonoBehaviour
{
    public static PlayerInfoManagerScript instance;

    public Vector3 currentRespawnPosition;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void UpdateRespawnPosition(Vector3 newPos)
    {
        currentRespawnPosition = newPos;
    }
}
