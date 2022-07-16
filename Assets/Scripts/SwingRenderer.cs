using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingRenderer : MonoBehaviour
{
    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform end;
    [SerializeField]
    private Transform rotator;
    private LineRenderer lr;

    public float offset;

   
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

    public void UpdateRenderer(Vector3 direction)
    {
        // I have no clue why this works, but it has to be this way.
        rotator.rotation = Quaternion.LookRotation(direction);
        rotator.rotation *= Quaternion.Euler(0, 90f, 0);
    }
}
