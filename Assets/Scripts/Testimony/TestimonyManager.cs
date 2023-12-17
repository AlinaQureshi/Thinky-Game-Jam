using System;
using System.Collections.Generic;
using UnityEngine;


public class TestimonyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Canvas _sketchCanva;
    [SerializeField] Canvas _noteCanva;
    [SerializeField] ContradictionScreen _contradictionScreen;

    [SerializeField] Transform _canvaParent;
    [SerializeField] Transform _testimonyParent;

    [Header("Testimonies")]
    [SerializeField] Testimony[] _testimonies;
    Dictionary<TestimonyItem, Testimony> _testimonyDict = new();

    Dictionary<TestimonyItem, TestimonyType> _testimonyAnswers = new();

    private TestimonyItem _currentItem;

    public static TestimonyManager Instance { get; private set; }

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
        UpdateDict();
    }

    public TestimonyType GetItemAnswer(TestimonyItem item)
    {
        return _testimonyAnswers[item];
    }

    public int GetTestimonyCount() { return _testimonies.Length; }

    void UpdateDict()
    {
        foreach (var testimony in _testimonies)
        {
            _testimonyDict[testimony.Item] = testimony;
            _testimonyAnswers[testimony.Item] = testimony.CorrectAnswer;
        }
    }

    public void UpdateCanvaOrder(TestimonyType type)
    {
        _noteCanva.sortingOrder = type == TestimonyType.Writing ? 2 : 1;
        _sketchCanva.sortingOrder = type == TestimonyType.Sketch ? 2 : 1;
    }

    public void CheckContradiction(DraggableUI firstUI, DraggableUI secondUI)
    {
        var firstItem = firstUI.GetTestimonyItem();
        var secondItem = secondUI.GetTestimonyItem();

        if (firstItem == secondItem)
        {
            firstUI.ItemDiscovered();
            secondUI.ItemDiscovered();
            ShowContradictionScreen(firstItem);

            FMOD.Studio.EventInstance audioEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Correct");
            audioEvent.start();
            audioEvent.release();
        }
        else
        {
            //Can add incorrect sfx here

            FMOD.Studio.EventInstance audioEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Incorrect");
            audioEvent.start();
            audioEvent.release();
        }
    }

    public void ShowContradictionScreen(TestimonyItem item, bool isNew = true)
    {
        _currentItem = item;
        var info = _testimonyDict[item];
        _contradictionScreen.UpdateScreenInfo(info.SketchInfo.Description, info.NoteInfo.Description);
        if (isNew) NotebookManager.Instance.ToggleNotebook(true);
        NotebookManager.Instance.GoToPage(6);
    }

    public void ChooseOption(int index)
    {
        _contradictionScreen.ToggleScreen(false);
        var info = _testimonyDict[_currentItem];
        DeductionManager.Instance.AddDeduction(_currentItem, (index == 0 ? info.SketchInfo : info.NoteInfo));
    }

    public Transform GetTestimonyTransform() { return _testimonyParent;}

    public Transform GetCanvaTransform() { return _canvaParent; }
}
