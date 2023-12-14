using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookAudio : MonoBehaviour
{
    [Header("Audio File Paths")]
    public string pageTurn;
    public string bookOpen;
    public string bookClose;
    public string writing;

    public void PlaySound(string audioFilepath)
    {
        FMOD.Studio.EventInstance audioEvent = FMODUnity.RuntimeManager.CreateInstance(audioFilepath);
        audioEvent.start();
        audioEvent.release();
    }
}
