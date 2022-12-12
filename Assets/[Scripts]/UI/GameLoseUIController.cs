using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoseUIController : MonoBehaviour
{

    private void Start()
    {
        SoundManager.instance.PlayMusic(MusicTracks.Stillness_of_the_night, 1.0f, true);
    }
    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("Main Manu");
    }

    public void OnReplayButtonPressed()
    {
        SceneManager.LoadScene("Level 1");
    }
}
