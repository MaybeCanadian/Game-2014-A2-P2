using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoseUIController : MonoBehaviour
{
    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("Main Manu");
    }

    public void OnReplayButtonPressed()
    {
        SceneManager.LoadScene("Level 1");
    }
}
