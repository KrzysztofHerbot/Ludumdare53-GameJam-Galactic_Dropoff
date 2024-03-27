using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class ShipLauncher : MonoBehaviour
{
    [SerializeField] private GameObject prefabShip;
    [SerializeField] private Transform originPoint;
    [SerializeField] AudioClip wooshSound;

    AudioSource ac;

    public float launchStrength = 8f;
    public float maxDistance = 2f;
    
    private GameObject dragTarget;
    private LineScript lr;
    
    public bool readyToShoot = true;

    void Start()
    {
        lr = GetComponentInChildren<LineScript>();
        ac = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckClick();
        }
        if (Input.GetMouseButton(0) && dragTarget != null)
        {
            ContinueDragging();
        }
        if (Input.GetMouseButtonUp(0) && dragTarget != null)
        {
            LaunchShip();
        }
    }

    void CheckClick()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0, 100f));

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Launcher"))
        {
            StartDragging(hit.collider.gameObject);
        }

    }

    void StartDragging(GameObject _dragTarget)
    {
        dragTarget = _dragTarget;
    }

    private Vector2 pullDirection;
    private float pullRatio;
    void ContinueDragging()
    {
        Vector2 targetPosition = GetMousePosition();
        Vector2 originPosition = originPoint.position;

        // Calculate the distance between the origin point and target position
        float distance = Vector2.Distance(targetPosition, originPosition);
        LookAtTarget(originPoint.gameObject);

        // Calculate the direction of the pull
        pullDirection = (targetPosition - originPosition).normalized;

        // Clamp max distance
        if (distance > maxDistance)
        {
            targetPosition = originPosition + pullDirection * maxDistance;
        }

        // Calculate the pull ratio based on the distance from the origin point
        pullRatio = Mathf.Clamp01(distance / maxDistance);

        // Move the drag target to the target position
        dragTarget.transform.position = targetPosition;
    }

    void LaunchShip()
    {
        GameObject newShipObject = Instantiate(prefabShip, dragTarget.transform.position, Quaternion.identity);
        Rigidbody2D newShipBody = newShipObject.GetComponent<Rigidbody2D>();
        Ship newShip = newShipObject.GetComponent<Ship>();
        
        newShipBody.velocity = launchStrength * pullDirection * pullRatio * -1;
        newShip.SnapTowardsHeading(newShipBody.velocity.normalized);

        //plays sound
        ac.PlayOneShot(wooshSound);

        //destroys line
        lr.DestroyLine();

        Destroy(dragTarget.gameObject);
        dragTarget = null;
    }

    private static Vector2 GetMousePosition()
    {
        Plane XYPlane = new Plane(Vector3.forward, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance;

        XYPlane.Raycast(ray, out distance);
        return (Vector2)ray.GetPoint(distance);
    }

    private void LookAtTarget(GameObject target)
    {
        // Get the direction from the current position to the target position
        Vector3 direction = target.transform.position - dragTarget.transform.position;

        // Calculate the angle of rotation needed to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the sprite to face the target
        dragTarget.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
