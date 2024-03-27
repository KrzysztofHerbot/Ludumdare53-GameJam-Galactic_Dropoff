using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousewheelDrag : MonoBehaviour
{
    [SerializeField] private float dragSpeed = 2f;

    private Camera cam;
    private Vector3 lastMousePos;

    void Start()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
            if (cam == null)
            {
                Debug.LogError("MouseDragCamera script requires a Camera component.");
                enabled = false;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 delta = cam.ScreenToWorldPoint(lastMousePos) - cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position += delta * dragSpeed;
            lastMousePos = Input.mousePosition;
        }
    }
}
