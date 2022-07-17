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

    [SerializeField]
    private float EndMaxDistance = 15f;

    private FollowCamera MainCamera;
   
    void Awake()
    {
        MainCamera = Camera.main.GetComponent<FollowCamera>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //calculates how far away the end needs to be from the dice depending on how charged the shot is
        float EndXPos = (MainCamera.CurrentCharge / MainCamera.MaxForce) * EndMaxDistance + start.localPosition.x;

        end.localPosition = new Vector3(EndXPos, 0, 0);

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
