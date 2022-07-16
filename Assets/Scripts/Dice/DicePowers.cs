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
    public void DieFace_Friction()
    {
        // Use random bit flip
    }

    // Increase or decrease bounciness
    public void DieFace_Bounciness()
    {
        // Use random bit flip
    }

    // Increase or decrease mass
    public void DieFace_Mass()
    {
        // Use random bit flip
    }

    // Increase or decrease mass
    public void DieFace_Drag()
    {
        // Use random bit flip
    }

    // Increase or decrease power meter speed
    public void DieFace_PowerMeterSpeed()
    {
        // Use random bit flip
    }

    // Be on fire and invincible, or be on fire and burn away
    public void DieFace_OnFire()
    {
        // Use random bit flip
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
