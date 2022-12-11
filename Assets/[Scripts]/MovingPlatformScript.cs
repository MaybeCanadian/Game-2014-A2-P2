using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    [Header("Movement Behaviours")]
    public e_PlatformMoveDirections direction;

    [Header("Preset Movement Variables")]
    public float verticalRange = 8.0f;

    public float horizontalRange = 8.0f;

    public float TimeToMove = 2.0f;

    [Header("Custum Movement Variables")]
    public List<Transform> patrolPoints;
    public float allowance = 0.1f;

    private int currentPathIndex     = 0;
    private List<Vector2> patrolPositions;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float timer;

    private Rigidbody2D rb;
    private void Start()
    {
        patrolPositions = new List<Vector2>();
        rb = GetComponent<Rigidbody2D>();
        timer = 0.0f;
        currentPathIndex = 0;
        startPosition = transform.position;
        DetermineEndPoint();
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
        timer += Time.deltaTime * 1.0f / TimeToMove;

        if (timer >= 1.0f) {
            currentPathIndex++;
            if(currentPathIndex >= patrolPositions.Count)
            {
                currentPathIndex = 0;
            }

            timer = 0.0f;
            endPosition = patrolPositions[currentPathIndex];
            startPosition = transform.position;
        
        }
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(startPosition, endPosition, timer);
    }
}