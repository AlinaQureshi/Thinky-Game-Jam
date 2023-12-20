using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeductionManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform _leftPageContainer;
    [SerializeField] Transform _rightPageContainer;
    [SerializeField] Deduction _deductionPrefab;

    [SerializeField] float _timelineUnlockDelay = 3f;
    [SerializeField] Button _timelineButton;
    [SerializeField] Image _timelineImage;

    [SerializeField]

    private int _deductionCount = 0;
    private int _numberOfDeductions;
    private int _deductionPerPage;

    Dictionary<TestimonyItem, Deduction> _currentDeductions = new();
    public static DeductionManager Instance { get; private set; }

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
        _numberOfDeductions = TestimonyManager.Instance.GetTestimonyCount();
        _deductionPerPage = _numberOfDeductions / 2;
        var newColor = _timelineImage.color;
        newColor.a = 0;
        _timelineImage.color = newColor;
    }

    public void AddDeduction(TestimonyItem item, TestimonyInfo info)
    {
        if (_currentDeductions.ContainsKey(item)) { _currentDeductions[item].Init(item, info);}
        else
        {
            var pageContainer = _deductionCount < _deductionPerPage ? _leftPageContainer : _rightPageContainer;
            _deductionCount++;

            var deduction = Instantiate(_deductionPrefab, pageContainer);
            _currentDeductions[item] = deduction;
            deduction.Init(item, info);  
        }

        if (_deductionCount >= _numberOfDeductions)
        {
            if (CheckDeductions()) StartCoroutine(DelayTimelineUnlock());
        } 
    }

    private bool CheckDeductions()
    {
        foreach (var item in _currentDeductions.Keys)
        {
            var answer = TestimonyManager.Instance.GetItemAnswer(item);
            if (_currentDeductions[item].GetTestimonyType() != answer) return false;
        }
        return true;
    }

    IEnumerator DelayTimelineUnlock()
    {
        yield return new WaitForSeconds(_timelineUnlockDelay);
        _timelineButton.interactable = true;
        var newColor = _timelineImage.color;
        newColor.a = 1;
        _timelineImage.color = newColor;
        NotebookManager.Instance.GoToPage(9);
    }

    
}
