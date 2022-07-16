using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

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
    [SerializeField]
    private float minVerticalAim;
    [SerializeField]
    private float maxVerticalAim;

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

    // Dice variables.
    private GameObject DiceObject;
    private DiceRoller roller;
    private SwingRenderer swingRenderer;
    
    // Camera variables.
    private Vector3 currentRotation;
    private PositionConstraint positionConstraint;
    private AimConstraint aimConstraint;
    private float verticalAim = 0;
    private bool freeCamEnabled = false;
    private float yRotation = 0f;
    private float xRotation = 0f;
    private Rigidbody rb;


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
            }
        }

        
        if (freeCamEnabled)
        {
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
            SwingForce += Charge * Time.deltaTime;
            SwingForce = Mathf.Clamp(SwingForce, 0, MaxCharge);
        }

        //Debug.Log(SwingForce);

        // Register Inputs
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //only sets to false in case we don't want the dice being hit in mid air
            if(!CanHitDiceInAir)
                CanTossDice = false;

            roller.ShouldSwing(SwingForce);
            SwingForce = 0;

            //using a couroutine otherwise the dice evaluates as it is being tossed
            StartCoroutine(SetCanEvaluate());
        }

        verticalAim += Input.GetAxis("Mouse Y") * cameraSensitivity;
        verticalAim = Mathf.Clamp(verticalAim, minVerticalAim, maxVerticalAim);

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

        Vector3 direction = transform.forward * verticalMovement + transform.right * horizontalMovement;
        rb.AddForce(direction.normalized * freeCamMoveSpeed * 10f, ForceMode.Force);

        if (Input.GetKey(KeyCode.E))
        {
            rb.AddForce(Vector3.up * freeCamMoveSpeed * 10f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddForce(Vector3.down * freeCamMoveSpeed * 10f, ForceMode.Force);
        }
    }

    IEnumerator SetCanEvaluate()
    {
        yield return new WaitForSeconds(0.5f);

        DiceObject.GetComponent<DiceSolver>().CanEvaluate = true;

        Debug.Log("ready");
    }

    private void PrepareInformationForSwingRenderer()
    {
        Vector3 direction = transform.position - DiceObject.transform.position;
        direction.y = verticalAim;
        swingRenderer.UpdateRenderer(direction.normalized);
    }

    private void PositionSwingCamera()
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

        CanTossDice = true;

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