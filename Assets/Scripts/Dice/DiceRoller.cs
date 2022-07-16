using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    private Rigidbody rb;
    private bool shouldSwing;

    [SerializeField]
    private Transform swingDirection;

    private float Force;

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

    public void ShouldSwing(float SwingForce)
    {
        shouldSwing = true;
        Force = SwingForce;
    }

    private void Swing()
    {
        float Max = 500;
        float Min = 100;

        // IDK why it has to be swingDirection.right, but it does.
        rb.AddForce(swingDirection.right * Force, ForceMode.Impulse);
        rb.AddTorque(new Vector3(UnityEngine.Random.Range(Min, Max), UnityEngine.Random.Range(Min, Max), UnityEngine.Random.Range(Min, Max)));
    }
}
