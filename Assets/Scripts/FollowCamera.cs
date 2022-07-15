using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject DiceObject;
    [SerializeField]
    private Vector3 cameraOffset;
    private DiceRoller roller;

    // Start is called before the first frame update
    void Start()
    {
        DiceObject = GameObject.FindGameObjectsWithTag("Dice")[0];
        roller = DiceObject.GetComponent<DiceRoller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            roller.ShouldSwing();
        }

        transform.position = DiceObject.transform.position;
        transform.position = cameraOffset + transform.position;
    }
}
