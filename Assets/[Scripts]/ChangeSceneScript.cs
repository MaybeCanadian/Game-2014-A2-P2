using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//--------------------------------------------
//ChangeSceneScript.cs - Evan Coffey - 101267129
//
//Allows user to easily change the scene with space bar.
//
//latest revision - 11/20/2022
//
//Revision History- 
//11/20/2022
//--------------------------------------------


public class ChangeSceneScript : MonoBehaviour
{
    public string nextScene;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
