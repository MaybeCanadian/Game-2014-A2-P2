using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerScript : MonoBehaviour
{
    public UIName tutorialWindow;
    bool triggered = false;

    private void Start()
    {
        triggered = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(triggered)
        {
            return;
        }

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if(player)
        {
            triggered = true;
            player.ShowTutorial(tutorialWindow);
        }
    }
}
