using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Slider healthBarSlider;

    [Header("Movement Properties")]
    public float horizontalSpeed;
    public Transform groundPoint;
    public Transform aheadPoint;
    public Transform frontPoint;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;
    public bool isGroundAhead;
    public bool isObstacleAhead;
    public Vector2 direction;

    private void Start()
    {
        healthBarSlider = GetComponentInChildren<Slider>();
        currentHealth = maxHealth;
        UpdateHealthBar();
        direction = Vector2.left;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        SoundManager.instance.PlaySFX(SFXList.Hit);
        UpdateHealthBar();

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        healthBarSlider.value = currentHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        isObstacleAhead = Physics2D.Linecast(groundPoint.position, frontPoint.position, groundLayerMask);
        isGroundAhead = Physics2D.Linecast(groundPoint.position, aheadPoint.position, groundLayerMask);

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);

        if (isGrounded && isGroundAhead)
        {
            Move();
        }
        if (isGrounded && (!isGroundAhead || isObstacleAhead))
        {
            Flip();
        }
    }

    public void Move()
    {
        transform.position += new Vector3(direction.x * horizontalSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    public void Flip()
    {
        var x = transform.localScale.x * -1.0f;
        direction *= -1.0f;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);

        Gizmos.DrawLine(groundPoint.position, frontPoint.position);
        Gizmos.DrawLine(groundPoint.position, aheadPoint.position);
    }
}
