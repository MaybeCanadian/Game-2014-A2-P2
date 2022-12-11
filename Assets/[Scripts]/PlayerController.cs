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
    public float defaultGravityScale;

    [Header("Coyote Frames")]
    public float coyoteDuration;
    public bool coyoteTime;
    public float jumpCooldown;
    public bool jumpOnCooldown;

    [Header("Multiple Jumps")]
    public int numberOfJumps = 1;
    public bool inAir = false;
    public int currentJumpNumber;

    [Header("Wall Jumps")]
    public bool wallJumpEnabled;
    public Transform wallPoint;
    public float wallRadius;
    public bool isTouchingWall;
    public LayerMask wallJumpLayerMask;
    public float wallJumpForceUp;
    public float wallJumpForceSide;
    public bool right;

    [Header("Particle Effects")]
    public ParticleSystem particleSystem;
    public float jumpEffectDuration;
    public Color jumpEffectColor;
    public float runEffectDuration;
    public Color runEffectColor;

    [Header("Physics")]
    public Rigidbody2D rb;

    [Header("Control Scheme")]
    public bool useMobileInput = false;

    [Header("Player Animations")]
    public Animator anims;
    private Vector2 savedInputs;

    //------------------------
    //Init functions
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
        anims = GetComponent<Animator>();
        savedInputs = new Vector2(0.0f, 0.0f);
        rb.gravityScale = defaultGravityScale;
        isGrounded = false;
        isGroundedLast = false;
        jumpOnCooldown = false;
        currentJumpNumber = 0;
        right = true;
    }
    //-----------------------
    //Movement Functions
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
            inAir = false;
            if (coyoteTime)
            {
                StopCoroutine(CoyoteTimeCoroutine());
                coyoteTime = false;
            }
        }

        Vector2 input =  (useMobileInput) ? GetMobileInput() : GetKeyboardInput();

        if(input.x > 0)
        {
            right = true;
        }
        else if(input.x < 0)
        {
            right = false;
        }

        Move(input.x);
        ParseJumpInput(input.y);
        GravityAdjust();

        isGroundedLast = isGrounded;

        savedInputs = input;
    }

    private void Update()
    {
        ControlAnimations(savedInputs);
    }
    private void Move(float x)
    {
        if (x != 0.0f)
        {
            Flip(x);
            rb.AddForce(Vector2.right * x * horizontalForce * ((isGrounded) ? 1.0f : airFactor));

            var clampedXVeclocity = Mathf.Clamp(rb.velocity.x, -horizontalSpeed, horizontalSpeed);

            rb.velocity = new Vector2(clampedXVeclocity, rb.velocity.y);

            if (isGrounded)
            {
                PlayRunningParticleEffect();
            }
        }
    }
    private void ParseJumpInput(float y)
    {
        if (!jumpOnCooldown && y > 0.0f)
        {
            if ((isGrounded || coyoteTime))
            {
                currentJumpNumber = 0;
                inAir = true;
                Jump(firstJumpForce);
            }
            else if (isTouchingWall && wallJumpEnabled)
            {
                currentJumpNumber = 0;
                WallJump(wallJumpForceUp, wallJumpForceSide);
            }
            else if (currentJumpNumber < numberOfJumps && inAir)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                Jump(extraJumpForce);
            }
        }
    }
    private void Jump(float force)
    {
        if(transform.parent != null)
        {
            Rigidbody2D rbParent = transform.parent.GetComponent<Rigidbody2D>();

            if(rbParent != null)
            {
                Debug.Log("combined velocity");
                rb.velocity += rbParent.velocity;
            }
        }

        StopCoroutine("ResetJumpCooldown");
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        jumpOnCooldown = true;
        Invoke("ResetJumpCooldown", jumpCooldown);

        //PlayJumpingParticleEffect();
        currentJumpNumber++;
    }
    private void WallJump(float upForce, float sideForce)
    {
        rb.velocity = new Vector2(0.0f, 0.0f);
        float direction = (right) ? 1.0f : -1.0f;
        rb.AddForce(Vector2.up * upForce + Vector2.left * direction * sideForce, ForceMode2D.Impulse);
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
        if (rb.velocity.y < 0.0f)
        {
            rb.gravityScale += gravityBoostRate * Time.fixedDeltaTime;
            return;
        }

        rb.gravityScale = defaultGravityScale;

    }
    //-----------------------
    //Input Collection
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
    //-----------------------
    //Player Animations
    private void ControlAnimations(Vector2 inputDirection)
    {
        if(inputDirection.x != 0 && isGrounded)
        {
            anims.SetInteger("AnimState", (int)playerAnimStates.Run);
            return;
        }

        if(!isGrounded && !coyoteTime)
        { 
            if(isTouchingWall && wallJumpEnabled)
            {
                anims.SetInteger("AnimState", (int)playerAnimStates.Wall);
                return;
            }

            if(rb.velocity.y < 0)
            {
                anims.SetInteger("AnimState", (int)playerAnimStates.Fall);
                return;
            }

            anims.SetInteger("AnimState", (int)playerAnimStates.Jump);
            return;
        }

        anims.SetInteger("AnimState", (int)playerAnimStates.Idle);
        return;

    }
    //-----------------------
    //Particle Effects
    private void PlayRunningParticleEffect()
    {
        //particleSystem.GetComponent<Renderer>().material.SetColor("_Color", runEffectColor);
        particleSystem.Play();
    }
    //-----------------------
    //Corutines
    private IEnumerator CoyoteTimeCoroutine()
    {
        float timer = 0;
        
        while(timer <= coyoteDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        coyoteTime = false;
        yield break;
    }
    //-----------------------
    //Gizmo Draws
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallPoint.position, wallRadius);
    }
    //-----------------------
}

public enum playerAnimStates
{
    Idle,
    Run,
    Jump,
    Fall,
    Wall,
    Die
}
