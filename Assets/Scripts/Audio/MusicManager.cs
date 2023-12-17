using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    const string masterBusPath = "bus:/";
    const string sfxBusPathl = "bus:/SFX";
    const string musicBusPath = "bus:/Music";

    const string musicMenuPath = "event:/Frame_of_Mind_Menu";
    const string musicGamePath = "event:/Frame_of_Mind";

    private FMOD.Studio.EventInstance musicGameEvent;
    private FMOD.Studio.EventInstance musicMenuEvent;

    // Start is called before the first frame update
    void Start()
    {
        PlayMainGameplayMusic();
    }

    private void PlayMainGameplayMusic()
    {
        musicGameEvent = FMODUnity.RuntimeManager.CreateInstance(musicGamePath);
        musicGameEvent.start();
    }

    private void PlayMainMenuMusic()
    {
        musicMenuEvent = FMODUnity.RuntimeManager.CreateInstance(musicMenuPath);
        musicMenuEvent.start();
    }

    public void StopGameplayMusic()
    {
        musicGameEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicGameEvent.release();
    }

    public void StopMainMenuMusic()
    {
        musicMenuEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicMenuEvent.release();
    }
}
