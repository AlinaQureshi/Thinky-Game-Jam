using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    private FMOD.Studio.Bus masterBus;
    private FMOD.Studio.Bus musicBus;
    private FMOD.Studio.Bus sfxBus;

    public UnityEngine.UI.Slider masterSlider;
    public UnityEngine.UI.Slider musicSlider;
    public UnityEngine.UI.Slider sfxSlider;


    // Start is called before the first frame update
    void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
    }

    public void ChangeMasterVolume()
    {
        masterBus.setVolume(masterSlider.value);
    }

    public void ChangeMusicVolume()
    {
        musicBus.setVolume(musicSlider.value);
    }

    public void ChangeSFXVolume()
    {
        sfxBus.setVolume(sfxSlider.value);
    }
}
