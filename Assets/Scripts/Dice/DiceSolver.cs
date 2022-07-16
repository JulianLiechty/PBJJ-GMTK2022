using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSolver : MonoBehaviour
{
    [Header("Becareful with these values \nDon't change unless necessary")]

    [SerializeField]
    private int Forward = 1;
    [SerializeField]
    private int Down = 2;
    [SerializeField]
    private int Right = 3;
    [SerializeField]
    private int Left = 4;
    [SerializeField]
    private int Back = 5;
    [SerializeField]
    private int Up = 6;

    private bool ShouldEvaluate = false;

    public delegate int DiceValue(int Val);
    public event DiceValue OnDiceStop;

    private DicePowers diePowers;

    private void Start()
    {
        diePowers = GetComponent<DicePowers>();
    }

    private void Update()
    {
        int value = CheckDiceStop();

        if(value != 0 && ShouldEvaluate)
        {
            OnDiceStop(value);
            ApplyPowersFromFaceUp(value);
            ShouldEvaluate = false;
        }
    }

    /// <summary>
    /// Gets the value of the dice once it has stop moving
    /// </summary>
    public int CheckDiceStop()
    {
        float VelocityThreshold = 0.1f;

        if(GetComponent<Rigidbody>().velocity.magnitude < VelocityThreshold)
        {
            return GetUpSide();
        }

        return 0;
    }

    public int GetUpSide()
    {
        //Debug code if needed
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1000, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1000, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1000, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1000, Color.red);

        //ray casts in all 6 directions

        diePowers.ResetPowersToDefault();
        if (RayCastFromDice(transform.TransformDirection(Vector3.forward)))
        {
            return Forward;
        }

        if (RayCastFromDice(transform.TransformDirection(Vector3.down)))
        {
            return Down;
        }

        if (RayCastFromDice(transform.TransformDirection(Vector3.right)))
        {
            return Right;
        }

        if (RayCastFromDice(transform.TransformDirection(Vector3.left)))
        {
            return Left;
        }

        if (RayCastFromDice(transform.TransformDirection(Vector3.back)))
        {
            return Back;
        }

        if (RayCastFromDice(transform.TransformDirection(Vector3.up)))
        {
            return Up;
        }

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

    /// <summary>
    /// 
    /// </summary>
    private void ApplyPowersFromFaceUp(int value)
    {
        diePowers.ResetPowersToDefault();
        if (value == Forward)
        {
            diePowers.Face4Power();
        }

        if (value == Down)
        {
            diePowers.Face6Power();
        }

        if (value == Right)
        {
            diePowers.Face5Power();
        }

        if (value == Left)
        {
            diePowers.Face3Power();
        }

        if (value == Back)
        {
            diePowers.Face2Power();
        }

        if (value == Up)
        {
            diePowers.Face1Power();
        }
    }

    public bool CanEvaluate
    {
        get
        {
            return ShouldEvaluate;
        }

        set
        {
            ShouldEvaluate = value;
        }
    }
}
