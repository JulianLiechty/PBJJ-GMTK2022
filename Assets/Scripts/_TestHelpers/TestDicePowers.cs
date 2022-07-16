using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Use the number keys to test the 6 dice powers
 * For now, have a text message that confirms the use of powers
 * 1-6 = dice powers
 * 0 = reset to normal
 */
public class TestDicePowers : MonoBehaviour
{
    [SerializeField] private DicePowers powers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NumberPressPowers();
    }

    private void NumberPressPowers()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            RestoreToDefaultPower();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Face1Power();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Face2Power();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Face3Power();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Face4Power();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Face5Power();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Face6Power();
        }
    }

    private void Face1Power()
    {
        var powerUp = powers.DieFace_Friction();

        if (powerUp)
        {
            Debug.Log("Friction has increased.");
        }
        else
        {
            Debug.Log("Friction has decreased.");
        }
    }

    private void Face2Power()
    {
        var powerUp = powers.DieFace_Bounciness();

        if (powerUp)
        {
            Debug.Log("Bounciness has increased.");
        }
        else
        {
            Debug.Log("Bounciness has decreased.");
        }
    }

    private void Face3Power()
    {
        var powerUp = powers.DieFace_Mass();

        if (powerUp)
        {
            Debug.Log("Mass has increased.");
        }
        else
        {
            Debug.Log("Mass has decreased.");
        }
    }

    private void Face4Power()
    {
        var powerUp = powers.DieFace_Drag();

        if (powerUp)
        {
            Debug.Log("Drag has increased.");
        }
        else
        {
            Debug.Log("Drag has decreased.");
        }
    }

    private void Face5Power()
    {
        var powerUp = powers.DieFace_PowerMeterSpeed();

        if (powerUp)
        {
            Debug.Log("Power Meter Speed has increased.");
        }
        else
        {
            Debug.Log("Power Meter Speed has decreased.");
        }
    }

    private void Face6Power()
    {
        var powerUp = powers.DieFace_OnFire();

        if (powerUp)
        {
            Debug.Log("You're on fire and invincible.");
        }
        else
        {
            Debug.Log("You're on fire and hindered.");
        }
    }

    private void RestoreToDefaultPower()
    {
        Debug.Log("Your die has returned to normal");
    }
}
