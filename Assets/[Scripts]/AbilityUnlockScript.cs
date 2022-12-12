using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUnlockScript : MonoBehaviour
{
    public Ability abilityToUnlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if(player)
        {
            player.UnlockAbility(abilityToUnlock);
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public enum Ability
{
    WALL_JUMP,
    DOUBLE_JUMP
}