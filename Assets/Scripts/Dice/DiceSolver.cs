using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSolver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Gets the value of the dice once it has stop moving
    /// </summary>
    private int OnDiceStop()
    {
        float VelocityThreshold = 0.1f;

        if(GetComponent<Rigidbody>().velocity.magnitude < VelocityThreshold)
        {
            return GetUpSide();
        }

        return 0;
    }

    private int GetUpSide()
    {
        //Debug code if needed
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1000, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1000, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1000, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1000, Color.red);

        //ray casts in all 6 directions

        if (RayCastFromDice(transform.TransformDirection(Vector3.forward)))
            return 1;

        if (RayCastFromDice(transform.TransformDirection(Vector3.down)))
            return 2;

        if (RayCastFromDice(transform.TransformDirection(Vector3.right)))
            return 3;

        if (RayCastFromDice(transform.TransformDirection(Vector3.left)))
            return 4;

        if (RayCastFromDice(transform.TransformDirection(Vector3.back)))
            return 5;

        if (RayCastFromDice(transform.TransformDirection(Vector3.up)))
            return 6;

        return 0;
    }

    /// <summary>
    /// Creates a raycast from the dice into a direction and checks if it hit the ground
    /// </summary>
    /// <param name="Direction"></param>
    /// <returns></returns>
    private bool RayCastFromDice(Vector3 Direction)
    {
        float RayCastDistance = 10;
        RaycastHit Hit;

        if (Physics.Raycast(transform.position, Direction, out Hit, RayCastDistance))
        {
            return Hit.collider.gameObject.CompareTag("Ground");
        }

        return false;
    }
}
