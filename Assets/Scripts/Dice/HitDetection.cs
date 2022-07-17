using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private BoxCollider diceCollider;

    public delegate void DiceHit();
    public event DiceHit DiceHitEvent;
    // Start is called before the first frame update
    void Start()
    {
        diceCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Hitable Stuff"))
        {
            DiceHitEvent();
        }
    }
}
