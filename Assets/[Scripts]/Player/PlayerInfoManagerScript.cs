using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//--------------------------------
//PlayerInforManagerScript - Evan Coffey 101267129
//Handles most player stats like health and lives
public class PlayerInfoManagerScript : MonoBehaviour
{
    public static PlayerInfoManagerScript instance;
    public float Timer = 0.0f;
    public Vector3 currentRespawnPosition;

    public float MaxHealth;
    public float CurrentHealth;
    public float CoinsCollected;

    public int StartingLives;
    public int LivesRemaining;

    public TMP_Text coinCollectedText;
    public TMP_Text livesLeftText;

    public Slider healthBar;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        LivesRemaining = StartingLives;
        Timer = 0.0f;
        CurrentHealth = MaxHealth;
        CoinsCollected = 0;
        livesLeftText.text = LivesRemaining.ToString();
        SoundManager.instance.PlayMusic(MusicTracks.Confrontations_in_the_shadows, 1.0f, true);
        UpdateHealthBar();
        StartCoroutine(ActivateTimer());
    }

    private IEnumerator ActivateTimer()
    {
        while (true)
        {
            Timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    private void Update()
    {
        Timer += Time.deltaTime;
    }

    public void UpdateRespawnPosition(Vector3 newPos)
    {
        currentRespawnPosition = newPos;
    }

    private void UpdateHealthBar()
    {
        healthBar.value = CurrentHealth / MaxHealth;
    }
    public void CollectCoin()
    {
        CoinsCollected++;
        coinCollectedText.text = CoinsCollected.ToString();
        SoundManager.instance.PlaySFX(SFXList.Coin);
    }

    public void Respawn(GameObject player)
    {
        if(LivesRemaining > 0)
        {
            player.transform.position = currentRespawnPosition;
            LivesRemaining--;
            livesLeftText.text = LivesRemaining.ToString();
            CurrentHealth = MaxHealth;
            UpdateHealthBar();
        }
        else
        {
            StopAllCoroutines();
            SceneManager.LoadScene("Game Lose Screen");
        }
    }

    public bool TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        UpdateHealthBar();

        if(CurrentHealth <= 0)
        {
            return true;
        }

        return false;
    }
}
