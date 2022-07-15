using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingRenderer : MonoBehaviour
{
    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform end;
    private LineRenderer lr;

   
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, start.position);
        lr.SetPosition(1, end.position);
    }
}
