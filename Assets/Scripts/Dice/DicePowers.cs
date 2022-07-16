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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
