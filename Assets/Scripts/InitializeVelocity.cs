using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InitializeVelocity : MonoBehaviour
{
    public bool initializeVelocity = false;
    public Vector2 initialVelocity;
    // Start is called before the first frame update
    void Start()
    {
        // Do nothing if we are disabled
        if (!initializeVelocity) return;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = initialVelocity;
    }
}
