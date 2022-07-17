using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardsPlayer : MonoBehaviour
{
    GameObject cameraObject;
    // Start is called before the first frame update
    void Start()
    {
        cameraObject = GameObject.FindGameObjectsWithTag("MainCamera")[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraObject.transform);
    }
}
