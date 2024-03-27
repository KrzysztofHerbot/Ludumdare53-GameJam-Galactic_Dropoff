using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{

    public static readonly float TORQUE_MULTIPLIER = 5f;

    public bool touchingSomething;
    public int packagesLaunched = 0;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!touchingSomething) RotateTowardsHeading();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        touchingSomething = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        touchingSomething = false;
    }

    public void SnapTowardsHeading(Vector2 direction)
    {
        // Calculate the angle of rotation in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Create a new rotation quaternion
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Set the rotation of the ship's transform
        transform.rotation = rotation;
    }

    private void RotateTowardsHeading()
    {
        // Get the velocity of the rigidbody
        Vector2 velocity = rb.velocity;

        // Calculate the direction in which the ship needs to be rotated
        Vector2 direction = velocity.normalized;

        // Calculate the angle of rotation in radians
        float angle = Mathf.Atan2(direction.y, direction.x);

        // Calculate the difference between the current rotation and the desired rotation using Mathf.DeltaAngle
        float rotationDiff = Mathf.DeltaAngle(rb.rotation, angle * Mathf.Rad2Deg);

        // Apply torque to the Rigidbody2D component to rotate the ship
        rb.AddTorque(rotationDiff * TORQUE_MULTIPLIER);
    }
}
