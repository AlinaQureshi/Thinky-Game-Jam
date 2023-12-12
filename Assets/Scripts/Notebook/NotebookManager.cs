using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] Animator _animator;
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
    }

}
