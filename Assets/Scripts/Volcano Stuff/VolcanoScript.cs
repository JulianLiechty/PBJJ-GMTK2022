using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoScript : MonoBehaviour
{
    [SerializeField]
    private GameObject winScreen;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Dice"))
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("Event:/AMB/AMB_Volcano", this.gameObject);
            winScreen.SetActive(true);
        }
    }
}
