using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMatch : MonoBehaviour
{
    [SerializeField] int _correctOrder;

    [SerializeField]  int _currentOrder;

    private void Start()
    {
        _currentOrder = -1;
    }

    public void AddEvent(int currentEvent)
    {
        _currentOrder = currentEvent;
    }

    public bool CheckEvent()
    {
        return _currentOrder == _correctOrder;
    }
}
