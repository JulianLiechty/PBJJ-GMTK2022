using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Vector3 m_NewPosition;
    // his is the new X position. Set it in the Inspector.
    public float m_XPosition;

    // Use this for initialization
    void Start()
    {
        // Initialise the vector
        m_NewPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        // Press the space key to change the x component of the vector
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_NewPosition.x = m_XPosition;
            m_NewPosition.x = 10;
        }
        // Change the position depending on the vector
        transform.position = m_NewPosition;
    }
}
