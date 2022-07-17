using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    private Rigidbody rb;
    private bool shouldSwing;

    [SerializeField]
    private Transform swingDirection;

    [SerializeField] private float maxForce = 1000f;
    [SerializeField] private float minForce = 100f;

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
        float Max = maxForce;
        float Min = minForce;

        // IDK why it has to be swingDirection.right, but it does.
        rb.AddForce(swingDirection.right * Force, ForceMode.Impulse);
        rb.AddTorque(new Vector3(UnityEngine.Random.Range(Min, Max), UnityEngine.Random.Range(Min, Max), UnityEngine.Random.Range(Min, Max)));
    }
}
