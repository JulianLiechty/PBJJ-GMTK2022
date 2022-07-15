using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    private Rigidbody rb;
    private bool shouldSwing;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (shouldSwing)
        {
            shouldSwing = false;
            Swing();
        }
    }

    public void ShouldSwing()
    {
        shouldSwing = true;
    }

    private void Swing()
    {
        Debug.Log("Space key was pressed.");
        rb.AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
    }
}
