using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] NotebookFlipper _notebookFlipper;

    [SerializeField] NotebookAudio _notebookAudioRef;
    const string OPEN_PARAMETER = "IsOpen";

    public static NotebookManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ToggleNotebook(bool open)
    {
        _animator.SetBool(OPEN_PARAMETER, open);

        if (open)
        {
            _notebookAudioRef.PlaySound(_notebookAudioRef.bookOpen);
        }
        else
        {
            _notebookAudioRef.PlaySound(_notebookAudioRef.bookClose);
        }
    }

    public void GoToPage(int page)
    {
        _notebookFlipper.GotoPage(page);
    }

}
