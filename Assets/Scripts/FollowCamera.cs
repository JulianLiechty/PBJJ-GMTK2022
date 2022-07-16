using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

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
    private Vector3 currentRotation;
    PositionConstraint positionConstraint;
    AimConstraint aimConstraint;
    private float constraintY;

    // Start is called before the first frame update
    void Start()
    {
        DiceObject = GameObject.FindGameObjectsWithTag("Dice")[0];
        roller = DiceObject.GetComponent<DiceRoller>();
        Cursor.lockState = CursorLockMode.Locked;

        CreateConstraints();
    }

    private void CreateConstraints()
    {
        positionConstraint = GetComponent<PositionConstraint>();
        aimConstraint = GetComponent<AimConstraint>();
        currentRotation = new Vector3(cameraFollowDistance, cameraFollowHeight, 0);
        ConstraintSource constSrc = new ConstraintSource();
        constSrc.sourceTransform = DiceObject.transform;
        constSrc.weight = 1f;
        positionConstraint.AddSource(constSrc);
        aimConstraint.AddSource(constSrc);
        constraintY = currentRotation.y;
        currentRotation.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Register Inputs
        if (Input.GetKeyDown(KeyCode.Space))
        {
            roller.ShouldSwing();
        }
        PositionCamera();
    }

    private void PositionCamera()
    {
        float rotation = Input.GetAxis("Mouse X") * cameraSensitivity;
        RotateConstraint(rotation);
    }

    public void RotateConstraint(float angle)
    {
        float x = currentRotation.x * Mathf.Cos(angle) - currentRotation.z * Mathf.Sin(angle);
        float z = currentRotation.x * Mathf.Sin(angle) + currentRotation.z * Mathf.Cos(angle);
        currentRotation = new Vector3(x, constraintY, z);
        positionConstraint.translationOffset = currentRotation;
    }
}