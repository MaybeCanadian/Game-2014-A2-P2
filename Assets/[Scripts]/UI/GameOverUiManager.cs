using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUiManager : MonoBehaviour
{

    private void Start()
    {
        SoundManager.instance.PlayMusic(MusicTracks.Confrontations_in_the_shadows, 1.0f, true);
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
