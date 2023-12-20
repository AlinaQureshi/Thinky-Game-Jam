using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] EventBlock[] _eventBlocks;

    [SerializeField] EventMatch[] _eventMatches;

    Dictionary<EventBlock, EventMatch> _eventDict = new();

    int _currentEventFound;

    public static TimelineManager Instance { get; private set; }

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

    public void ConnectEvents(EventBlock block, EventMatch match)
    {
        if (IsOccupied(match))
        {
            block.ResetBackToLocalPosition();
            return;
        }

        block.SnapToPosition(match.transform.position);
        _eventDict[block] = match;
        match.AddEvent(block.GetOrder());
        
        if (CheckWinCondition())
        {
            NotebookManager.Instance.GoToPage(11);
        }
    }

    public void ResetDictKey(EventBlock block)
    {
        _eventDict[block] = null;
    }

    public bool CheckWinCondition()
    {
        int count = 0;
        foreach (var eventKey in _eventDict.Keys)
        {
            if (_eventDict[eventKey] == null) return false;
            count++;
            var check = _eventDict[eventKey].CheckEvent();
            if (check == false)
            {
                return false;
            }
        }
        return count >= _eventBlocks.Length;
    }

    public bool IsOccupied(EventMatch match)
    {
        return _eventDict.Values.Contains(match);
    }

    public void UpdateEvent(string text, int order)
    {
        _eventBlocks[_currentEventFound].UpdateEvent(text, order);
        _currentEventFound++;
    }


}
