using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool hasLOS = false;
    public bool inRange = false;
    public float awarenessRange = 10.0f;

    public LayerMask detectionLayer;
    public LayerMask lineOfSightBlockingLayers;
    public Transform playerTransform;

    public float attackCoolDown = 1.0f;
    public bool attackInCooldown = false;
    public float attackRange = 1.0f;
    public float attackArc = 90.0f;
    public float attackDamage = 5.0f;

    public bool isDead;
    public EnemyController controller;

    public Animator anims;

    private void Start()
    {
        hasLOS = false;
        inRange = false;
        attackInCooldown = false;
        anims = GetComponent<Animator>();
        controller = GetComponent<EnemyController>();
    }

    private void FixedUpdate()
    {
        if(inRange)
        {
            InRange();
            return;
        }

        Collider2D player = Physics2D.OverlapCircle(transform.position, awarenessRange, detectionLayer);

        if(player)
        {
            inRange = true;
            playerTransform = player.transform;
        }
    }
    private void InRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(controller.aheadPoint.position, playerTransform.position, lineOfSightBlockingLayers);
        Debug.Log(hit.collider.name);
        PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();

        if (player)
        {
            hasLOS = true;
        }
        else
        {
            hasLOS = false;
        }
    }
    private void Update()
    {
        if (inRange)
        {
            if (hasLOS && (transform.position - playerTransform.position).magnitude < attackRange && !attackInCooldown)
            {
                anims.SetInteger("AnimState", (int)EnemyAnimStates.Attack);
                attackInCooldown = true;
                Invoke("ResetAttackCoolDown", attackCoolDown);
            }
        }
    }

    private void ResetAttackCoolDown()
    {
        attackInCooldown = false;
    }

    public void OnAttackEvent()
    {
        Collider2D[] collidersHit = Physics2D.OverlapCircleAll(transform.position, attackRange, detectionLayer);
        float attackDirection = transform.localScale.x / Mathf.Abs(transform.localScale.x);

        foreach (Collider2D col in collidersHit)
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

    public void OnAttackFinishedEvent()
    {
        if(!controller.isDead)
        {
            anims.SetInteger("AnimState", (int)EnemyAnimStates.Walk);
        }
    }

    private void HitTarget(Collider2D col)
    {
        PlayerCollisionScript player = col.gameObject.GetComponent<PlayerCollisionScript>();

        if(player)
        {
            player.TakeDamage(attackDamage);
        }
    }
}

public enum EnemyAnimStates
{
    Idle,
    Walk,
    Attack,
    Die,
    Hit
}
