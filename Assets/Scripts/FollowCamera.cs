using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject Dice;
    [SerializeField]
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        Dice = GameObject.FindGameObjectsWithTag("Dice")[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Dice.transform.position;
        transform.position = cameraOffset + transform.position;
    }
}
