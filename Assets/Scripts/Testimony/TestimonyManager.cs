using System;
using System.Collections.Generic;
using UnityEngine;


public class TestimonyManager : MonoBehaviour
{
    [SerializeField] Canvas _sketchCanva;
    [SerializeField] Canvas _noteCanva;

    [SerializeField] ContradictionScreen _contradictionScreen;

    [SerializeField] Testimony[] _testimonies;
    Dictionary<TestimonyItem, Testimony> _testimonyDict = new();

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

    void UpdateDict()
    {
        foreach (var testimony in _testimonies)
        {
            _testimonyDict[testimony.Item] = testimony;
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
            UpdateContradictionScreen(firstItem);
        }
        else
        {
            //Can add incorrect sfx here
        }
    }

    public void UpdateContradictionScreen(TestimonyItem item)
    {
        var info = _testimonyDict[item];
        _contradictionScreen.UpdateScreenInfo(info.SketchInfo.Description, info.NoteInfo.Description);
    }

    public void ChooseOption(int index)
    {
        _contradictionScreen.ToggleScreen(false);
    }
}
