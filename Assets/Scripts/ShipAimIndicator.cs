using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAimIndicator : MonoBehaviour
{
    public Vector2 GetDirection()
    {
        return transform.forward;
    }

    void Update()
    {
        Plane XYPlane = new Plane(Vector3.forward, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance;

        // Find the point where the mouse ray intersects the XY plane
        if (XYPlane.Raycast(ray, out distance))
        {
            // Get the intersection point
            Vector3 intersectionPoint = ray.GetPoint(distance);

            // Calculate the direction vector from the object's position to the intersection point
            Vector3 direction = intersectionPoint - transform.position;

            // Point that direction
            transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
        }
    }
}
