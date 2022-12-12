using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatformScript : MonoBehaviour
{
    public GameObject partnerPlatform;

    public Rigidbody2D rb;

    public Vector3 startPosition;
    public Vector3 partnerStartPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        partnerStartPosition = partnerPlatform.transform.position;
    }

    private void Update()
    {
        float displacement = transform.position.y - startPosition.y;

        partnerPlatform.transform.position = partnerStartPosition + new Vector3(0.0f, -displacement, 0.0f);
    }
}
