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
    public float customSpeedFactor = 0.3f;

    private int currentPathIndex     = 0;
    private List<Vector2> patrolPositions;
    private Vector2 startPosition;
    private Vector2 destinationPoint;
    private float timer;
    private void Start()
    {
        patrolPositions = new List<Vector2>();
        timer = 0.0f;
        currentPathIndex = 0;
        startPosition = transform.position;
        foreach(Transform pathPoint in patrolPoints)
        {
            Vector2 point = pathPoint.position;

            patrolPositions.Add(point);
        }

        patrolPositions.Add(transform.position);

        destinationPoint = patrolPositions[currentPathIndex];
    }

    private void FixedUpdate()
    {
        if(direction == e_PlatformMoveDirections.CUSTOM)
        {
            if(timer <= 1.0f)
            {
                timer += customSpeedFactor;
            }
            else if(timer >= 1.0f)
            {
                timer = 0.0f;
                currentPathIndex++;

                if(currentPathIndex >= patrolPositions.Count)
                {
                    currentPathIndex = 0;
                }

                startPosition = transform.position;
                destinationPoint = patrolPositions[currentPathIndex];
            }
        }
    }
    private void Update()
    {
        Move();
    }
    public void Move()
    {
        switch (direction)
        {
            case e_PlatformMoveDirections.HORIZONTAL:
                MoveHorizontal();
                break;
            case e_PlatformMoveDirections.VERTICAL:
                MoveVertical();
                break;
            case e_PlatformMoveDirections.DIAGONAL_UP_RIGHT:
                MoveDiagonal_UP();
                break;
            case e_PlatformMoveDirections.DIAGONAL_UP_LEFT:
                MoveDiagonal_Down();
                break;
            case e_PlatformMoveDirections.CUSTOM:
                MoveCustom();
                break;
        }
    }

    public void MoveHorizontal()
    {
        transform.position = new Vector2(Mathf.PingPong(horizontalSpeed * Time.time, horizontalRange) + startPosition.x,
            startPosition.y);
    }

    private void MoveVertical()
    {
        transform.position = new Vector2(startPosition.x,
            Mathf.PingPong(verticalSpeed * Time.time, verticalRange) + startPosition.y);
    }

    private void MoveDiagonal_UP()
    {
        transform.position = new Vector2(Mathf.PingPong(horizontalSpeed * Time.time, horizontalRange) + startPosition.x,
           Mathf.PingPong(verticalSpeed * Time.time, verticalRange) + startPosition.y);
    }

    private void MoveDiagonal_Down()
    {
        transform.position = new Vector2(Mathf.PingPong(horizontalSpeed * Time.time, horizontalRange) + startPosition.x,
           -Mathf.PingPong(verticalSpeed * Time.time, verticalRange) + startPosition.y);
    }

    private void MoveCustom()
    {
        transform.position = Vector2.Lerp(startPosition, destinationPoint, timer);
    }
}
