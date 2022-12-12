using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    PlayerController controller;
    PlayerAnimationScript anims;

    public float deathInvulTime = 1.0f;
    bool Invul = false;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        anims = GetComponent<PlayerAnimationScript>();
        Invul = false;
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

    public void OnDeathAnimFinished()
    {
        if (Invul == false)
        {
            controller.isDead = false;
            PlayerInfoManagerScript.instance.Respawn(gameObject);
            Invul = true;
            Invoke("ResetInvul", deathInvulTime);
        }
    }

    private void ResetInvul()
    {
        Invul = false;
    }
}
