using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    [SerializeField]
    private GameObject winScreen;
    private void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("Event:/AMB/AMB_Volcano", this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Dice"))
        {
            winScreen.SetActive(true);
        }
    }
}
