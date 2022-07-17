using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float xForce;
    public float yForce = 500f;
    public float zForce;
    public float multiplier = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(xForce, yForce, zForce));
            Debug.Log("bouncin'");
        }
        if (collision.gameObject.CompareTag("BounceOnPad"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(xForce, yForce * multiplier, zForce));
            Debug.Log("bouncin'");
        }
    }
}
