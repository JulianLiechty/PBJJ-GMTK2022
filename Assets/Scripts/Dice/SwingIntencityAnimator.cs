using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingIntencityAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private GameObject CameraObject;
    void Awake()
    {
        CameraObject = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        CameraObject.GetComponent<FollowCamera>().SwingForcePercentage += OnSwingForceIntensityChange;
    }

    private void OnSwingForceIntensityChange(float percentagePower)
    {
        animator.SetFloat("Charging", percentagePower);
        Debug.Log("Percentage power: " + percentagePower);
    }

    //animator.SetTrigger("Launch");
    //animator.SetTrigger("Hit");
}
