using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoManagerScript : MonoBehaviour
{
    public static PlayerInfoManagerScript instance;

    public Vector3 currentRespawnPosition;

    public float MaxHealth;
    public float CurrentHealth;
    public float CoinsCollected;

    public int StartingLives;
    public int LivesRemaining;

    public TMP_Text coinCollectedText;
    public TMP_Text livesLeftText;

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
        LivesRemaining = StartingLives;
        CurrentHealth = MaxHealth;
        CoinsCollected = 0;
        livesLeftText.text = LivesRemaining.ToString();
    }
    public void UpdateRespawnPosition(Vector3 newPos)
    {
        currentRespawnPosition = newPos;
    }
    public void CollectCoin()
    {
        CoinsCollected++;
        coinCollectedText.text = CoinsCollected.ToString();
    }

    public void Respawn(GameObject player)
    {
        if(LivesRemaining > 0)
        {
            player.transform.position = currentRespawnPosition;
            LivesRemaining--;
            livesLeftText.text = LivesRemaining.ToString();
        }
    }
}
