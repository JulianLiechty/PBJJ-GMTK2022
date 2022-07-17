using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VolumeManager : MonoBehaviour
{
    public float volume { get; private set; }

    //Instance of the menu manager that remains alive the entire life of the application.
    public static VolumeManager instance;
    public GameObject instanceObject;

    public void SetVolume(float newVol)
    {
        volume = newVol;
    }

    private void Awake()
    {
        if(instance != null)
        {
            this.volume = instance.volume;
            Destroy(instance.instanceObject);
        }

        instance = this;
    }
}
