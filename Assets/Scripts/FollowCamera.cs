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
    private PositionConstraint positionConstraint;
    private AimConstraint aimConstraint;
    private float constraintY;
    private SwingRenderer swingRenderer;

    private void Awake()
    {
        // Grab the constraint components from the camera.
        positionConstraint = GetComponent<PositionConstraint>();
        aimConstraint = GetComponent<AimConstraint>();

        DiceObject = GameObject.FindGameObjectsWithTag("Dice")[0];

        // roller and swing renderer so that camera can push, and rotate the swing UI.
        swingRenderer = DiceObject.GetComponentInChildren<SwingRenderer>();
        roller = DiceObject.GetComponent<DiceRoller>();
    }

    // Start is called before the first frame update
    void Start()
    { 
        // Lock cursor to game window to make the camera feel better to use.
        Cursor.lockState = CursorLockMode.Locked;

        CreateConstraints();
    }

    /// <summary>
    /// Adds the constraints to the camera so that it always aims at, and follows the dice object.
    /// </summary>
    private void CreateConstraints()
    {
        // Create the camera's offset from the dice using the editor values.
        currentRotation = new Vector3(cameraFollowDistance, cameraFollowHeight, 0);

        // Create a new constraint source and set the transform to the dice, and a weight of 1.
        ConstraintSource constSrc = new ConstraintSource();
        constSrc.sourceTransform = DiceObject.transform;
        constSrc.weight = 1f;

        // Add the constraint source to the position and aim constraints to actually enable the constraints.
        positionConstraint.AddSource(constSrc);
        aimConstraint.AddSource(constSrc);

        // Remove the y variable for rotation of the camera in the RotateConstraint() method.
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

        PrepareInformationForSwingRenderer();
        PositionCamera();
    }

    private void PrepareInformationForSwingRenderer()
    {
        Vector3 direction = transform.position - DiceObject.transform.position;
        direction.y = 0;
        swingRenderer.UpdateRenderer(direction.normalized);
    }

    private void PositionCamera()
    {
        // Get the amount of rotation required for the camera.
        float rotation = Input.GetAxis("Mouse X") * cameraSensitivity;
        // Rotate the constraints to create the rotation effect.
        RotateConstraint(rotation);
    }

    /// <summary>
    /// Rotates the x and z values of the constraints to create horizontal rotation around the dice.
    /// </summary>
    /// <param name="angle">How much to turn.</param>
    public void RotateConstraint(float angle)
    {   
        // Rotate the vector representing the rotation of the camera.
        // I pretended the rotation was a 2d vector and rotated that.
        float x = currentRotation.x * Mathf.Cos(angle) - currentRotation.z * Mathf.Sin(angle);
        float z = currentRotation.x * Mathf.Sin(angle) + currentRotation.z * Mathf.Cos(angle);
        
        // Store the new rotation value.
        currentRotation = new Vector3(x, cameraFollowHeight, z);

        // Set the new constraint.
        positionConstraint.translationOffset = currentRotation;
    }
}