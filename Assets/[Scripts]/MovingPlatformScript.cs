using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    [Header("Movement Behaviours")]
    public e_PlatformMoveDirections direction;

    [Header("Preset Movement Variables")]
    public float verticalSpeed = 3.0f;
    public float verticalRange = 8.0f;

    public float horizontalSpeed = 3.0f;
    public float horizontalRange = 8.0f;

    [Header("Custum Movement Variables")]
    public List<Transform> patrolPoints;
    public float allowance = 0.1f;

    private int currentPathIndex     = 0;
    private List<Vector2> patrolPositions;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float timer;

    private Rigidbody2D rb;

    public List<Rigidbody2D> connectedRigidBodies;
    private void Start()
    {
        patrolPositions = new List<Vector2>();
        connectedRigidBodies = new List<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        timer = 0.0f;
        currentPathIndex = 0;
        startPosition = transform.position;
        DetermineEndPoint();
        MoveTowardsNextPosition();
    }

    private void DetermineEndPoint()
    {
        Debug.Log(direction);
        switch(direction)
        {
            case e_PlatformMoveDirections.VERTICAL:
                patrolPositions.Add(startPosition + new Vector2(0.0f, verticalRange));
                
                break;
            case e_PlatformMoveDirections.HORIZONTAL:
                patrolPositions.Add(startPosition + new Vector2(horizontalRange, 0.0f));
                break;
            case e_PlatformMoveDirections.DIAGONAL_UP_RIGHT:
                patrolPositions.Add(startPosition + new Vector2(horizontalRange, verticalRange));
                break;
            case e_PlatformMoveDirections.DIAGONAL_UP_LEFT:
                patrolPositions.Add(startPosition + new Vector2(-horizontalRange, verticalRange));
                break;
            case e_PlatformMoveDirections.CUSTOM:
                foreach (Transform pathPoint in patrolPoints)
                {
                    Vector2 point = pathPoint.position;

                    patrolPositions.Add(point);
                }
                break;
        }

        patrolPositions.Add(transform.position);

        Debug.Log(direction + patrolPositions.Count);
        endPosition = patrolPositions[currentPathIndex];
    }

    private void FixedUpdate()
    {
        if(((Vector2)transform.position - endPosition).magnitude < allowance) {
            currentPathIndex++;
            if(currentPathIndex >= patrolPositions.Count)
            {
                currentPathIndex = 0;
            }

            endPosition = patrolPositions[currentPathIndex];
            MoveTowardsNextPosition();

            foreach (Rigidbody2D rbOther in connectedRigidBodies)
            {
                rbOther.velocity += rb.velocity;
            }
        }
    }

    private void MoveTowardsNextPosition()
    {
        Vector3 moveDirection = (endPosition - (Vector2)transform.position).normalized;

        rb.velocity = new Vector3(moveDirection.x * horizontalSpeed, moveDirection.y * verticalSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rbOther = collision.gameObject.GetComponent<Rigidbody2D>();
        collision.transform.parent = transform;
        if (rbOther) 
        {
            connectedRigidBodies.Add(rbOther);
            rbOther.velocity = rb.velocity;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D rbOther = collision.gameObject.GetComponent<Rigidbody2D>();
        collision.transform.parent = null;
        if (rbOther)
        {
            connectedRigidBodies.Remove(rbOther);
            rbOther.velocity = rb.velocity;
        }
    }
}

public class moveChildren
{
    public GameObject connectedObject;
    public FixedJoint2D conectedJoint;
}
