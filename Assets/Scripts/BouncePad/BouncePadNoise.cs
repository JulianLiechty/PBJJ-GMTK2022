using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadNoise : MonoBehaviour
{
    [SerializeField]
    private GameObject pad;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Dice"))
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("Event:/SFX/SFX_SpringPad", pad);
        }
    }
}
