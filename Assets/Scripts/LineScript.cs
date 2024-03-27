using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField]private Transform[] pivotPoints;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        SetUpLine(pivotPoints,0.2f,0.2f);
    }

    public void SetUpLine(Transform[] pivotPoints,float start,float end)
    {
        //lr.enabled = true;
        lr.startWidth = start;
        lr.endWidth = end;
        lr.positionCount= pivotPoints.Length;
        this.pivotPoints = pivotPoints;
    }

    private void Update()
    {
        if (pivotPoints == null) { return; }
        for(int i=0;i<pivotPoints.Length;i++)
        {
            lr.SetPosition(i, pivotPoints[i].position);
        }
    }

    public void DestroyLine()
    {
        this.pivotPoints = null;
        lr.startWidth = 0f;
        lr.endWidth = 0f;
    }
}