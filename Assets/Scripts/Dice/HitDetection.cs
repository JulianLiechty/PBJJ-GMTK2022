using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    [SerializeField]
    private GameObject dice;
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
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/SFX_CollideGround", dice);
            DiceHitEvent();
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Rock"))
        {
            //FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/SFX_CollideRock", dice);
            DiceHitEvent();
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Bush"))
        {
            //FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/SFX_CollideBush", dice);
            DiceHitEvent();
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Wood"))
        {
            //FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/SFX_CollideWood", dice);
            DiceHitEvent();
        }

    }
}
