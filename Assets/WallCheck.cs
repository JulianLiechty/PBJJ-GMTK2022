using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    private void Awake()
    {
     //   Debug.Log(transform.rotation.eulerAngles.x + "X Rot");


        if ( (transform.rotation.eulerAngles.x > 30 || transform.rotation.eulerAngles.x < -30) || (transform.rotation.eulerAngles.z > 30 || transform.rotation.eulerAngles.z < -30) )
        {
            gameObject.tag = "Untagged";
        }
    }
}
