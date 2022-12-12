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
        SpikeScript spikes = collision.gameObject.GetComponent<SpikeScript>();

        if (spikes)
        {
            Debug.Log("Hit spikes");
            controller.isDead = true;
            anims.PlayDeathAnimation();
            return;
        }

        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if(enemy)
        {
            SoundManager.instance.PlaySFX(SFXList.Hurt);
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

    public void TakeDamage(float damage)
    {
        SoundManager.instance.PlaySFX(SFXList.Hurt);
        if(PlayerInfoManagerScript.instance.TakeDamage(damage))
        {
            anims.PlayDeathAnimation();
        }
    }
}
