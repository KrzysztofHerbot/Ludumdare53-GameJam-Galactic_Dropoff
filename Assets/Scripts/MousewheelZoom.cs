using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousewheelZoom : MonoBehaviour
{
    private Camera cam;
    public float minSize = 20f;
    public float maxSize = 100f;
    [SerializeField] private float increment = 5f;


    public Transform background;
    public float backgroundSizeScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
            if (cam == null)
            {
                Debug.LogError("MousewheelZoom script requires a Camera component.");
                enabled = false;
            }
        }
        else if (!cam.orthographic)
        {
            Debug.LogError("MousewheelZoom script requires an orthographic Camera.");
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = -1f * Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + scroll * increment, minSize, maxSize);
        }

        background.localScale = Vector3.one * cam.orthographicSize * backgroundSizeScale;
    }
}