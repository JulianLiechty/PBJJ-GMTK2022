using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class FollowCamera : MonoBehaviour
{
    // Swing camera settings.
    [SerializeField]
    private float cameraSensitivity;
    [SerializeField]
    private float cameraFollowDistance;
    [SerializeField]
    private float cameraFollowHeight;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float minVerticalAimIndicator;
    [SerializeField]
    private float maxVerticalAimIndicator;
    [SerializeField]
    private float minVerticalAimCamera;
    [SerializeField]
    private float maxVerticalAimCamera;


    [SerializeField]
    private float Charge = 2f;
    [SerializeField]
    private float MaxCharge = 50f;
    private float SwingForce = 0f;

    [SerializeField]
    [Tooltip("Set to true in case players can hit the dice while airborne")]
    private bool CanHitDiceInAir = false;
    private bool CanTossDice = true;

    // Free camera settings.
    [SerializeField]
    private float freeCamMoveSpeed;
    [SerializeField]
    private float freeCamLookSensitivity;
    [SerializeField]
    private float sprintMultiplier;

    // Dice variables.
    private GameObject DiceObject;
    private DiceRoller roller;
    private SwingRenderer swingRenderer;

    // Camera variables.
    private Vector3 currentRotation;
    private PositionConstraint positionConstraint;
    private AimConstraint aimConstraint;
    private float swingIndicatorVerticalAngle = 0;
    private bool freeCamEnabled = false;
    private float yRotation = 0f;
    private float xRotation = 0f;
    private Rigidbody rb;
    private bool sprinting = false;

    // Gameplay Variables that shouldn't be here, but game jam.
    [SerializeField]
    private int numPowerupAirJumps;
    [SerializeField]
    [Range(1, 6)]
    private int faceValueThatAllowsAirJumps;
    private int airJumpsUsed = 0;
    [SerializeField]
    private float airJumpForceMultiplier;

    [SerializeField]
    private float EvaluationInterval = 2f;
    private bool charging;

    // Event announcing the current value of the swing intensity.
    public delegate void SwingIntensity(float Val);
    public event SwingIntensity SwingForcePercentage;

    // Dice Launched
    public delegate void DiceLaunched();
    public event DiceLaunched DiceLaunchedEvent;

    private void Awake()
    {
        // Grab the constraint components from the camera.
        positionConstraint = GetComponent<PositionConstraint>();
        aimConstraint = GetComponent<AimConstraint>();

        rb = GetComponent<Rigidbody>();

        DiceObject = GameObject.FindGameObjectsWithTag("Dice")[0];

        // roller and swing renderer so that camera can push, and rotate the swing UI.
        swingRenderer = DiceObject.GetComponentInChildren<SwingRenderer>();
        roller = DiceObject.GetComponent<DiceRoller>();

        //Binds the stop event on the dice to a function in this script
        DiceObject.GetComponent<DiceSolver>().OnDiceStop += DiceStop;
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
        // Toggle Free Cam.
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (freeCamEnabled)
            {
                freeCamEnabled = false;
                positionConstraint.enabled = true;
                aimConstraint.enabled = true;
            }
            else
            {
                freeCamEnabled = true;
                positionConstraint.enabled = false;
                aimConstraint.enabled = false;
                transform.LookAt(DiceObject.transform);
                Quaternion rotation = transform.rotation;
                xRotation = rotation.eulerAngles.x;
                yRotation = rotation.eulerAngles.y;
            }
        }


        if (freeCamEnabled)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                sprinting = true;
            if (Input.GetKeyUp(KeyCode.LeftShift))
                sprinting = false;

            float mouseX = Input.GetAxisRaw("Mouse X") * freeCamLookSensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * freeCamLookSensitivity;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

            // If free cam is enabled, then the rest of the scripts should not run.
            return;
        }


        if (Input.GetKey(KeyCode.Space) && CanTossDice)
        {
            charging = true;
            SwingForce += Charge * Time.deltaTime;

            //makes the charge ping pong between max and min value
            if (SwingForce > MaxCharge || SwingForce < 0)
                Charge = -Charge;

            SwingForce = Mathf.Clamp(SwingForce, 0, MaxCharge);
            if(SwingForcePercentage is not null)
                SwingForcePercentage(SwingForce / MaxCharge);

            roller.SwingStrengthChanged();
        }

        //Debug.Log(SwingForce);

        // Register Inputs
        if (Input.GetKeyUp(KeyCode.Space) && CanTossDice)
        {
            //only sets to false in case we don't want the dice being hit in mid air
            if (!CanHitDiceInAir || airJumpsUsed >= numPowerupAirJumps)
                CanTossDice = false;

            if (CanTossDice && CanHitDiceInAir && airJumpsUsed > 1)
                roller.ShouldSwing(SwingForce * airJumpForceMultiplier);
            else
                roller.ShouldSwing(SwingForce);

            if(DiceLaunchedEvent is not null)
                DiceLaunchedEvent();
            airJumpsUsed++;
            SwingForce = 0;

            //using a couroutine otherwise the dice evaluates as it is being tossed
            StartCoroutine(SetCanEvaluate());
        }

        swingIndicatorVerticalAngle += Input.GetAxis("Mouse Y") * cameraSensitivity;
        swingIndicatorVerticalAngle = Mathf.Clamp(swingIndicatorVerticalAngle, minVerticalAimIndicator, maxVerticalAimIndicator);

        PrepareInformationForSwingRenderer();
        PositionSwingCamera();
    }

    private void FixedUpdate()
    {
        // Only mess with the rigidbody when using free cam.
        if (!freeCamEnabled)
            return;

        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        float movementSpeed;
        if (sprinting)
            movementSpeed = freeCamMoveSpeed * 10f * sprintMultiplier;
        else
            movementSpeed = freeCamMoveSpeed * 10f;

        Vector3 direction = transform.forward * verticalMovement + transform.right * horizontalMovement;
        rb.AddForce(direction.normalized * movementSpeed, ForceMode.Force);

        if (Input.GetKey(KeyCode.E))
        {
            rb.AddForce(Vector3.up * movementSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddForce(Vector3.down * movementSpeed, ForceMode.Force);
        }
    }

    IEnumerator SetCanEvaluate()
    {
        yield return new WaitForSeconds(EvaluationInterval);

        DiceObject.GetComponent<DiceSolver>().CanEvaluate = true;
    }

    private void PrepareInformationForSwingRenderer()
    {
        Vector3 direction = transform.position - DiceObject.transform.position;
        direction.y = swingIndicatorVerticalAngle;
        swingRenderer.UpdateRenderer(direction.normalized);
    }

    private void PositionSwingCamera()
    {
        // Get the vertical and horizontal rotation for the camera.
        float hRotation = Input.GetAxis("Mouse X") * cameraSensitivity;
        float vRotation = Input.GetAxis("Mouse Y") * cameraSensitivity;


        // If the swing is charging, and there is some form of rotation.
        if (charging && hRotation != 0)
            roller.AimChanged();

        // Rotate the constraints to create the horizontal orbital rotation effect.
        RotateConstraint(hRotation, vRotation);
    }

    /// <summary>
    /// Rotates the x and z values of the constraints to create horizontal rotation around the dice.
    /// </summary>
    /// <param name="hAngle">How much to turn horizontally.</param>
    /// <param name="vAngle">How much to turn vertically.</param>
    public void RotateConstraint(float hAngle, float vAngle)
    {
        // Horizontal movement

        // Rotate the vector representing the rotation of the camera.
        // I pretended the rotation was a 2d vector and rotated that.
        float x = currentRotation.x * Mathf.Cos(hAngle) - currentRotation.z * Mathf.Sin(hAngle);
        float z = currentRotation.x * Mathf.Sin(hAngle) + currentRotation.z * Mathf.Cos(hAngle);

        // Vertical movement
        currentRotation.y += vAngle;
        currentRotation.y = Mathf.Clamp(currentRotation.y, minVerticalAimCamera, maxVerticalAimCamera);

        // Store the new rotation value.
        currentRotation = new Vector3(x, currentRotation.y, z);

        // Set the new constraint.
        positionConstraint.translationOffset = currentRotation;
        transform.position = DiceObject.transform.position + currentRotation;
    }

    /// <summary>
    /// Method bound to Dice Stop Event on the Dice
    /// </summary>
    /// <param name="Val"></param>
    /// <returns></returns>
    private int DiceStop(int Val)
    {
        Debug.Log(Val);
        if (CanHitDiceInAir)
        {
            CanHitDiceInAir = false;
        }
            
        

        CanTossDice = true;

        if (Val == faceValueThatAllowsAirJumps)
        {
            CanHitDiceInAir = true;
            airJumpsUsed = 0;
        }
            

        return 0;
    }

    public float CurrentCharge
    {
        get
        {
            return SwingForce;
        }
    }

    public float MaxForce
    {
        get
        {
            return MaxCharge;
        }
    }
}