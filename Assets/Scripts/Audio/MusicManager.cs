using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    const string musicMenuPath = "event:/Frame_of_Mind_Menu";
    const string musicStingerPath = "event:/Frame_of_Mind_Stinger";
    const string musicGamePath = "event:/Frame_of_Mind";

    private FMOD.Studio.EventInstance musicGameEvent;
    private FMOD.Studio.EventInstance musicMenuEvent;
    private FMOD.Studio.EventInstance musicStingerEvent;

    public EventInstance MusicGameEvent { get => musicGameEvent; }
    public EventInstance MusicMenuEvent { get => musicMenuEvent; }
    public EventInstance MusicStingerEvent { get => musicStingerEvent; }

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

    private void PlayStingerMusic()
    {
        musicStingerEvent = FMODUnity.RuntimeManager.CreateInstance(musicStingerPath);
        musicStingerEvent.start();
    }

    public void StopMusic(FMOD.Studio.EventInstance musicEvent) 
    {
        if (!musicEvent.isValid()) return;

        musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicEvent.release();
    }
}
