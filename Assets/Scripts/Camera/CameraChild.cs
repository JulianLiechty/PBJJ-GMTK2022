using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChild : MonoBehaviour
{
    [SerializeField]
    private SphereCollider parentCollider;
    private SphereCollider childCollider;

    void Start()
    {
        childCollider = GetComponent<SphereCollider>();
        Physics.IgnoreCollision(parentCollider, childCollider);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.zero;
    }
}
