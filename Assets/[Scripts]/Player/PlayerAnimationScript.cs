using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    public Animator anims;
    private Vector2 savedInputs;

    public PlayerController controller;
    public Rigidbody2D rb;

    public Vector2 savedDirection;
    public bool isAttacking = false;

    private void Start()
    {
        anims = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
        isAttacking = false;
    }
    private void PlayAnimation(playerAnimStates animation)
    {
        anims.SetInteger("AnimState", (int)animation);
    }
    private void Update()
    {
        ControlAnimations(savedDirection);
    }
    public void PlayAttackAnimation()
    {
        if (controller.inAir)
        {
            PlayAnimation(playerAnimStates.JumpAttack);
        }
        else
        {
            PlayAnimation(playerAnimStates.Attack);
        }

        isAttacking = true;
        return;
    }
    public void PlayDeathAnimation()
    {
        PlayAnimation(playerAnimStates.Die);
    }
    public void AttackFinished()
    {
        isAttacking = false;
    }
    private void ControlAnimations(Vector2 inputDirection)
    {
        if(isAttacking)
        {
            return;
        }

        if (controller.isDead)
        {
            PlayAnimation(playerAnimStates.Die);
            return;
        }

        if (inputDirection.x != 0 && controller.isGrounded)
        {
            PlayAnimation(playerAnimStates.Run);
            return;
        }

        if (!controller.isGrounded && !controller.coyoteTime)
        {
            if (controller.isTouchingWall && controller.wallJumpEnabled)
            {
                PlayAnimation(playerAnimStates.Wall);
                return;
            }

            if (rb.velocity.y < 0)
            {
                PlayAnimation(playerAnimStates.Fall);
                return;
            }

            PlayAnimation(playerAnimStates.Jump);
            return;
        }

        PlayAnimation(playerAnimStates.Idle);
        return;

    }
}
public enum playerAnimStates
{
    Idle,
    Run,
    Jump,
    Fall,
    Wall,
    Die,
    Attack,
    JumpAttack
}