using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePowerReset : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    void Start()
    {
        GameObject DiceObject = GameObject.FindGameObjectsWithTag("Dice")[0];
        DiceObject.GetComponent<DiceSolver>().OnDiceStop += OnSideChanged;
    }

    private int OnSideChanged(int side)
    {
        animator.SetTrigger("Reset");
        animator.SetInteger("FaceNumber", side);
        return 0;
    }
}
