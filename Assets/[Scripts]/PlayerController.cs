using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed Values")]
    public float horizontalForce;
    public float horizontalSpeed;
    public float firstJumpForce;
    public float extraJumpForce;
    public float airFactor;

    [Header("Ground Detection")]
    public Transform groundPoint;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;
    private bool isGroundedLast;

    [Header("Gravity Boosts")]
    public float gravityBoostRate;

    [Header("Coyote Frames")]
    public float coyoteFrames;
    public bool coyoteTime;
    public float jumpCooldown;
    public bool jumpOnCooldown;

    [Header("Multiple Jumps")]
    public int numberOfJumps = 1;
    public int currentJumpNumber;

    [Header("Wall Jumps")]
    public Transform wallPoint;
    public float wallRadius;
    public bool isTouchingWall;
    public LayerMask wallJumpLayerMask;
    public float wallJumpForceUp;
    public float wallJumpForceSide;


    [Header("Physcis")]
    public Rigidbody2D rb;

    [Header("Control Scheme")]
    public bool useMobileInput = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = false;
        isGroundedLast = false;
        jumpOnCooldown = false;
        currentJumpNumber = 0;
    }

    private void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;

        hit = Physics2D.OverlapCircle(wallPoint.position, wallRadius, wallJumpLayerMask);
        isTouchingWall = hit;
        
        if(!isGrounded && isGroundedLast)
        {
            coyoteTime = true;
            StartCoroutine(CoyoteTimeCoroutine());
        }

        if(isGrounded)
        {
            currentJumpNumber = 0;
            if (coyoteTime)
            {
                StopCoroutine(CoyoteTimeCoroutine());
                coyoteTime = false;
            }
        }

        Vector2 input =  (useMobileInput) ? GetMobileInput() : GetKeyboardInput();

        Move(input.x);
        ParseJumpInput(input.y);

        GravityAdjust();

        isGroundedLast = isGrounded;
    }

    private Vector2 GetMobileInput()
    {
        Vector2 input = new Vector2(0.0f, 0.0f);
        return input;
    }

    private Vector2 GetKeyboardInput()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"));

        return input;
    }

    private void Move(float x)
    {
        if (x != 0.0f)
        {
            Flip(x);
            rb.AddForce(Vector2.right * x * horizontalForce * ((isGrounded) ? 1.0f : airFactor));

            var clampedXVeclocity = Mathf.Clamp(rb.velocity.x, -horizontalSpeed, horizontalSpeed);

            rb.velocity = new Vector2(clampedXVeclocity, rb.velocity.y);
        }
        }

    private void ParseJumpInput(float y)
    {
        if (!jumpOnCooldown && y > 0.0f)
        {
            if ((isGrounded || coyoteTime))
            {
                currentJumpNumber = 0;
                Jump(firstJumpForce);
            }
            else if (isTouchingWall)
            {
                currentJumpNumber = 0;
                WallJump(wallJumpForceUp, wallJumpForceSide);
            }
            else if (currentJumpNumber < numberOfJumps)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                Jump(extraJumpForce);
            }
        }
    }

    private void Jump(float force)
    {
        StopCoroutine("ResetJumpCooldown");
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        jumpOnCooldown = true;
        Invoke("ResetJumpCooldown", jumpCooldown);
        currentJumpNumber++;
    }

    private void WallJump(float upForce, float sideForce)
    {
        rb.AddForce(Vector2.up * upForce + Vector2.left * sideForce, ForceMode2D.Impulse);
        jumpOnCooldown = true;
        Invoke("ResetJumpCooldown", jumpCooldown);
        currentJumpNumber++;
    }

    private void ResetJumpCooldown()
    {
        jumpOnCooldown = false;
    }

    public void Flip(float x)
    {
        if (x != 0.0f)
        {
            transform.localScale = new Vector3((x > 0.0f) ? 0.75f : -0.75f, 0.75f, 0.75f);
        }
    }

    private void GravityAdjust()
    {
        if(rb.velocity.y < 0.0f)
        {
            rb.gravityScale += gravityBoostRate * Time.fixedDeltaTime;
            return;
        }

        rb.gravityScale = 1.0f;

    }

    private IEnumerator CoyoteTimeCoroutine()
    {
        float timer = 0;
        
        while(timer <= coyoteFrames)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        coyoteTime = false;
        yield break;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallPoint.position, wallRadius);
    }
}
