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

    // Start is called before the first frame update
    void Start()
    {
        FMOD.Studio.EventInstance audioEvent = FMODUnity.RuntimeManager.CreateInstance(musicGamePath);
        audioEvent.start();
        audioEvent.release();
    }
}
