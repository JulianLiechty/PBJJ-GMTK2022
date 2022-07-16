using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Powers will affect the player's die depending on the roll
 */
public class DicePowers : MonoBehaviour
{
    public enum PowerAbilityType
    {
        Friction,
        Bounciness,
        Mass,
        Drag,
        PowerMeterSpeed,
        Fire
    }

    private Rigidbody rigidBody;
    private BoxCollider collider;
    public float defaultMass = 2f;
    public float defaultFriction = 0.5f;
    public float defaultBounciness = 0.5f;
    public float defaultGravityScale = 1f;
    public float moreMass = 4f;
    public float moreFriction = 0.75f;
    public float moreBounciness = 0.75f;
    public float moreGravity = 2f;
    public float lessMass = 1f;
    public float lessFriction = 0.25f;
    public float lessBounciness = 0.25f;
    public float lessGravity = 0.5f;

    private float gravityScale;


    /*
     * Dice powers based on face
     */
    public void Face1Power()
    {
        // Increase mass
        Debug.Log("Mass Increase.");
        rigidBody.mass = moreMass;
    }
    public void Face2Power()
    {
        // Increase bounciness
        Debug.Log("Bounciness Increase.");
        collider.material.dynamicFriction = lessFriction;
        collider.material.dynamicFriction = lessFriction;
        collider.material.bounciness = moreBounciness;
    }
    public void Face3Power()
    {
        // Increase friction
        Debug.Log("Gravity Increase.");
        gravityScale = moreGravity;
    }
    public void Face4Power()
    {
        // Decrease friction
        Debug.Log("Friction Decrease.");
        collider.material.dynamicFriction = lessFriction;
        collider.material.staticFriction = lessFriction;
    }
    public void Face5Power()
    {
        // Decrease bounciness
        Debug.Log("Gravity Decrease.");
        gravityScale = lessGravity;
    }
    public void Face6Power()
    {
        // Decrease mass
        Debug.Log("Mass Decrease.");
        rigidBody.mass = lessMass;
    }

    public void ResetPowersToDefault()
    {
        rigidBody.mass = defaultMass;
        gravityScale = defaultGravityScale;
        collider.material.bounciness = defaultBounciness;
        collider.material.dynamicFriction = defaultFriction;
        collider.material.staticFriction = defaultFriction;
    }

    /*
     * Dice powers will have a chance to increase or decrease based on dice roll
     */

    // Increae or decrease friction
    public bool DieFace_Friction()
    {
        int randomFlip = Random.Range(0, 2);
        // Use random bit flip
        return randomFlip % 2 == 1;
    }

    // Increase or decrease bounciness
    public bool DieFace_Bounciness()
    {
        int randomFlip = Random.Range(0, 2);
        // Use random bit flip
        return randomFlip % 2 == 1;
    }

    // Increase or decrease mass
    public bool DieFace_Mass()
    {
        int randomFlip = Random.Range(0, 2);
        // Use random bit flip
        return randomFlip % 2 == 1;
    }

    // Increase or decrease mass
    public bool DieFace_Drag()
    {
        int randomFlip = Random.Range(0, 2);
        // Use random bit flip
        return randomFlip % 2 == 1;
    }

    // Increase or decrease power meter speed
    public bool DieFace_PowerMeterSpeed()
    {
        int randomFlip = Random.Range(0, 2);
        // Use random bit flip
        return randomFlip % 2 == 1;
    }

    // Be on fire and invincible, or be on fire and burn away
    public bool DieFace_OnFire()
    {
        int randomFlip = Random.Range(0, 2);
        // Use random bit flip
        return randomFlip % 2 == 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        ResetPowersToDefault();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(Physics.gravity * gravityScale);
    }
}
