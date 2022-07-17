using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour
{
    [SerializeField]
    private float AngleThreshold = 30f;

    //private void Awake()
    //{
    //    //foreach (GameObject Platform in GameObject.FindGameObjectsWithTag("DefaultPlatform"))
    //    //{
    //    //    //if the platform is does *not* have enough of an angle we tag it with ground
    //    //    //if (!(Platform.transform.rotation.x > AngleThreshold || Platform.transform.rotation.z > AngleThreshold))
    //    //        Platform.tag = "Ground";
    //    //}
    //}

    private void Start()
    {
        foreach (GameObject Platform in GameObject.FindGameObjectsWithTag("DefaultPlatform"))
        {
            //if the platform is does *not* have enough of an angle we tag it with ground
            if (!(Platform.transform.rotation.x > AngleThreshold || Platform.transform.rotation.z > AngleThreshold))
                Platform.tag = "Ground";
        }
    }
}
