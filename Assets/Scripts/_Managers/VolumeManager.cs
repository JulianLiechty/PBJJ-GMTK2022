using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VolumeManager : MonoBehaviour
{
    //Instance of the menu manager that remains alive the entire life of the application.
    public static VolumeManager instance;
    public GameObject instanceObject;

    FMOD.Studio.EventInstance SFXVolumeTestEvent;

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Ambience;
    FMOD.Studio.Bus Master;
    float MusicVolume = 0.5f;
    float SFXVolume = 0.5f;
    float MasterVolume = 1f;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.instanceObject);
        }

        Music = FMODUnity.RuntimeManager.GetBus("bus:/MASTER/MUSIC");
        Ambience = FMODUnity.RuntimeManager.GetBus("bus:/MASTER/AMBIENCE");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/MASTER/SOUND EFFECTS");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/MASTER");
        SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SFX_DieRoll");

        instance = this;
    }

    void Update()
    {
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(MasterVolume);
        Ambience.setVolume(MusicVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
        Debug.Log("MV" + MasterVolume);
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
        Debug.Log("MusV" + MusicVolume);
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }
        Debug.Log("SF" + SFXVolume);
    }
}
