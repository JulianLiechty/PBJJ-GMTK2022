using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Variables that can be changed in the editor.
    [SerializeField]
    private float cameraSensitivity;
    [SerializeField]
    private float cameraFollowDistance;
    [SerializeField]
    private float cameraFollowHeight;
    [SerializeField]
    private float radius;

    private DiceRoller roller;
    private GameObject DiceObject;
    private float rotation;

    // Start is called before the first frame update
    void Start()
    {
        DiceObject = GameObject.FindGameObjectsWithTag("Dice")[0];
        roller = DiceObject.GetComponent<DiceRoller>();
        PositionCamera();
        rotation = 0;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Register Inputs
        if (Input.GetKeyDown(KeyCode.Space))
        {
            roller.ShouldSwing();
        }

        // Rotate the camera when the mouse is moved in the X direction.
        rotation = Input.GetAxis("Mouse X") * cameraSensitivity;
        PositionCamera();
    }

    private void PositionCamera()
    {
        Vector3 dicePosition = DiceObject.transform.position;
        transform.position = (transform.position - dicePosition).normalized * radius + dicePosition;
        transform.RotateAround(dicePosition, Vector3.up, rotation);
    }
}
