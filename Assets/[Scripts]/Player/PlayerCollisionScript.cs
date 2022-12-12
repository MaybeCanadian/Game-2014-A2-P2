using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    PlayerController controller;
    PlayerAnimationScript anims;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        anims = GetComponent<PlayerAnimationScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Debug.Log("Hit " + collision.gameObject.name);
        SpikeScript spikes = collision.gameObject.GetComponent<SpikeScript>();

        if (spikes)
        {
            Debug.Log("Hit spikes");
            controller.isDead = true;
            anims.PlayDeathAnimation();
        }
    }
}
