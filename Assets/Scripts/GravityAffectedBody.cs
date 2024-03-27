using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityAffectedBody : MonoBehaviour
{

    private static readonly float G = 1;

    GravityWell[] gravityWells;
    Rigidbody2D rb;
    void Start()
    {
        gravityWells = FindObjectsOfType<GravityWell>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        foreach (GravityWell gravityWell in gravityWells)
        {
            AddForceTowardsGravityWell(gravityWell);
        }
    }

    void AddForceTowardsGravityWell(GravityWell well)
    {
        Vector2 direction = well.transform.position - transform.position;
        float forceMagnitude = G * well.mass / direction.sqrMagnitude;
        Vector2 force = direction.normalized * forceMagnitude;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
