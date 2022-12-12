using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//--------------------------------------
//Player Attacking Scripts - 101278129 Evan Coffey
//Controls the player attacking
public class PlayerAttackingScript : MonoBehaviour
{
    public bool attackOnCoolDown = false;
    public float attackCoolDown = 3.0f;
    public float attackRange = 1.0f;
    public float attackArc = 90.0f;
    public LayerMask attackLayerMask;

    public bool useMobileInput;

    public PlayerAnimationScript anims;
    private void Start()
    {
        useMobileInput = Application.isMobilePlatform;
        anims = GetComponent<PlayerAnimationScript>();
        attackOnCoolDown = false;
    }

    private void Update()
    {
        if (!attackOnCoolDown)
        {
            if (useMobileInput)
            {
                if (MobileInputController.instance.AttackButtonDown)
                {
                    attackOnCoolDown = true;
                    anims.PlayAttackAnimation();
                    Invoke("ResetAttackCoolDown", attackCoolDown);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    attackOnCoolDown = true;
                    anims.PlayAttackAnimation();
                    Invoke("ResetAttackCoolDown", attackCoolDown);
                }
            }
        }
    }
    private void ResetAttackCoolDown()
    {
        attackOnCoolDown = false;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySFX(SFXList.Weapon_Whoosh);
        Collider2D[] collidersHit = Physics2D.OverlapCircleAll(transform.position, attackRange, attackLayerMask);
        float attackDirection = transform.localScale.x / Mathf.Abs(transform.localScale.x);

        foreach(Collider2D col in collidersHit)
        {
            Vector2 vectorToCollider = col.transform.position - transform.position;
            float VectorDot = Vector2.Dot(vectorToCollider.normalized, transform.right * attackDirection);
            //in referance to the dot product, 0 is 180 arc, 1 is 0 arc, -1 is 360 arc
            //0.5 is 90arc
            //-(x/180 - 1)
            float targetDot = -1.0f * ((attackArc / 180) - 1);
            //Debug.Log(targetDot);
            if (VectorDot > targetDot)
            {
                Debug.Log("We have hit " + col.gameObject.name);
                HitTarget(col);
            }
        }

    }
    private void HitTarget(Collider2D target)
    {
        EnemyController enemy = target.gameObject.GetComponent<EnemyController>();

        if(enemy)
        {
            enemy.TakeDamage(20.0f);
        }

        BreakableDoorScript door = target.gameObject.GetComponent<BreakableDoorScript>();

        if(door)
        {
            door.BreakDoor();
        }
    }
}

