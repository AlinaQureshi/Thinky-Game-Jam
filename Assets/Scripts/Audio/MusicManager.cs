using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FMOD.Studio.EventInstance audioEvent = FMODUnity.RuntimeManager.CreateInstance("event:/The_Metal_Rose");
        audioEvent.start();
        audioEvent.release();
    }
}
