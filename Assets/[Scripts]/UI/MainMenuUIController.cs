using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OnLoadButtonPressed()
    {
        Debug.Log("Not implemented yet.");
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
