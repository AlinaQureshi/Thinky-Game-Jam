using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] NotebookFlipper _notebookFlipper;

    [SerializeField] NotebookAudio _notebookAudioRef;
    const string OPEN_PARAMETER = "IsOpen";

    [SerializeField] Button _tab1;
    [SerializeField] Button _tab2;
    [SerializeField] Button _closetab;

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

    private void Start()
    {
        _tab1.interactable = false;
        _tab2.interactable = false;
        _closetab.interactable = false;
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

    public void StartBook()
    {
        _tab1.interactable = true;
        _tab2.interactable = true;
        _closetab.interactable=true;
        GoToPage(1);
    }
}
