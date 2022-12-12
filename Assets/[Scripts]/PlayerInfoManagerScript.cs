using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManagerScript : MonoBehaviour
{
    public static PlayerInfoManagerScript instance;

    public Vector3 currentRespawnPosition;

    public float MaxHealth;
    public float CurrentHealth;
    public float CoinsCollected;

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

    private void Start()
    {
        CurrentHealth = MaxHealth;
        CoinsCollected = 0;
    }
    public void UpdateRespawnPosition(Vector3 newPos)
    {
        currentRespawnPosition = newPos;
    }
    public void CollectCoin()
    {

    }
}
