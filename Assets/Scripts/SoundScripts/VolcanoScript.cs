using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoScript : MonoBehaviour
{
    private BoxCollider boxCollider;
    [SerializeField]
    private GameObject WinScreen;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Dice"))
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("Event:/AMB/AMB_Volcano", this.gameObject);
            WinScreen.gameObject.SetActive(true);
        }
    }
}
