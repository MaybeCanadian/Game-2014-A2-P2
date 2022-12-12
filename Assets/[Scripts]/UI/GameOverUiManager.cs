using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUiManager : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text coinsText;
    private void Start()
    {
        SoundManager.instance.PlayMusic(MusicTracks.Confrontations_in_the_shadows, 1.0f, true);
        timeText.text = PlayerInfoManagerScript.instance.Timer.ToString();
        coinsText.text = PlayerInfoManagerScript.instance.CoinsCollected.ToString();

    }
    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void OnNextLevelButonPressed()
    {
        Debug.Log("Not added yet");
    }

    public void OnReplayButtonPressed()
    {
        SceneManager.LoadScene("Level 1");
    }
}
