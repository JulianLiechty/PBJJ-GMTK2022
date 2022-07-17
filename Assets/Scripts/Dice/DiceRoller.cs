using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    private Rigidbody rb;
    private bool shouldSwing;

    [SerializeField]
    private Transform swingDirection;
    [SerializeField]
    private GameObject dice;
    private FMOD.Studio.EventInstance instance;
    private bool instanceStarted = false;

    [SerializeField] private float maxForce = 1000f;
    [SerializeField] private float minForce = 100f;

    [SerializeField]
    private float AimTickNoiseFrequencyInSeconds;
    private float timeSinceLastNoiseTick;
    private bool canTick = true;

    private float Force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeSinceLastNoiseTick = AimTickNoiseFrequencyInSeconds;
    }
    
    private void FixedUpdate()
    {
        if (shouldSwing)
        {
            shouldSwing = false;
            Swing();
        }
    }

    public void ShouldSwing(float SwingForce)
    {
        shouldSwing = true;
        Force = SwingForce;
    }

    private void Update()
    {
        if (!canTick)
        {
            timeSinceLastNoiseTick -= Time.deltaTime;

            if (timeSinceLastNoiseTick <= 0)
            {
                timeSinceLastNoiseTick = AimTickNoiseFrequencyInSeconds;
                canTick = true;
            }
        }
    }

    private void Swing()
    {
        float Max = maxForce;
        float Min = minForce;

        // IDK why it has to be swingDirection.right, but it does.
        rb.AddForce(swingDirection.right * Force, ForceMode.Impulse);
        rb.AddTorque(new Vector3(UnityEngine.Random.Range(Min, Max), UnityEngine.Random.Range(Min, Max), UnityEngine.Random.Range(Min, Max)));
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/SFX_DieRoll", dice);

        // Stop the audio from playing.
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
        instanceStarted = false;
    }

    public void AimChanged()
    {
        if (canTick)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/UI/UI_AimTick", dice);
            canTick = false;
        }
            
    }

    public void SwingStrengthChanged()
    {
        if (instanceStarted)
            return;

        instanceStarted = true;
        instance = FMODUnity.RuntimeManager.CreateInstance("Event:/UI/UI_PowerMeter");
        instance.start();
    }
}
