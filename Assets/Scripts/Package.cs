using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Package : MonoBehaviour
{
    public static readonly float COLLECT_FORCE_SCALAR = 15f;

    PackageGoal goal;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void BeCollectedBy(PackageGoal _goal)
    {
        this.goal = _goal;
    }

    private void FixedUpdate()
    {
        if (goal != null) MoveTowardsGoal();
    }

    private void MoveTowardsGoal()
    {
        rb.velocity *= 0.9f;
        Vector2 difference = goal.transform.position - transform.position;
        Vector2 force = difference.normalized * COLLECT_FORCE_SCALAR;
        rb.AddForce(force);
    }
}
