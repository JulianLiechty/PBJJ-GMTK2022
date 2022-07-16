using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    private Rigidbody rb;
    private bool shouldSwing;

    [SerializeField]
    private Transform swingDirection;

    public float Force;

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
        // IDK why it has to be swingDirection.right, but it does.
        rb.AddForce(swingDirection.right * Force, ForceMode.Impulse);
    }
}
