using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingIntencityAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private HitDetection diceHitDetector;
    private GameObject CameraObject;

    void Awake()
    {
        CameraObject = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        CameraObject.GetComponent<FollowCamera>().SwingForcePercentage += OnSwingForceIntensityChange;
        CameraObject.GetComponent<FollowCamera>().DiceLaunchedEvent += OnDiceLaunched;
        diceHitDetector.DiceHitEvent += OnDiceHit;
    }

    private void OnSwingForceIntensityChange(float percentagePower)
    {
        animator.SetFloat("Charging", percentagePower);
        Debug.Log("Percentage power: " + percentagePower);
    }

    private void OnDiceLaunched()
    {
        animator.SetTrigger("Launch");
    }

    private void OnDiceHit()
    {
        animator.SetTrigger("Hit");
    }


}
