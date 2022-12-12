using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUiManager : MonoBehaviour
{
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
