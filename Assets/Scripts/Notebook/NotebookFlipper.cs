using UnityEngine;
using System.Collections;
using System;
using BookCurlPro;


public class NotebookFlipper : MonoBehaviour
{
    [SerializeField] NotebookAudio _notebookAudioRef;
    [SerializeField] BookPro _notebookRef;
    [SerializeField] float _pageFlipTime = 1;
    [SerializeField] float _delayBeforeStart;
    [SerializeField] float _timeBetweenPages = 5;

    int _targetPaper;
    float _elapsedTime;
    float _nextPageCountDown;

    bool _isBookInteractable;
    bool _flippingStarted;
    bool _isPageFlipping;

    private FlipMode _mode;

    public void GotoPage(int pageNum)
    {
        if (pageNum < 0) pageNum = 0;
        if (pageNum > _notebookRef.papers.Length * 2) pageNum = _notebookRef.papers.Length * 2 - 1;
        this.enabled = true;
        _timeBetweenPages = 0;
        StartFlipping((pageNum + 1) / 2);
    }

    public void StartFlipping(int target)
    {
        

        _isBookInteractable = _notebookRef.interactable;
        _notebookRef.interactable = false;
        _flippingStarted = true;
        _elapsedTime = 0;
        _nextPageCountDown = 0;
        _targetPaper = target;
        if (target > _notebookRef.CurrentPaper) _mode = FlipMode.RightToLeft;
        else if (target < _notebookRef.currentPaper) _mode = FlipMode.LeftToRight;
    }

    void Update()
    {
        if (_flippingStarted)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _delayBeforeStart)
            {
                if (_nextPageCountDown < 0)
                {
                    if ((_notebookRef.CurrentPaper < _targetPaper && _mode == FlipMode.RightToLeft) || (_notebookRef.CurrentPaper > _targetPaper && _mode == FlipMode.LeftToRight))
                    {
                        _isPageFlipping = true;
                        PageFlipper.FlipPage(_notebookRef, _pageFlipTime, _mode, () => { _isPageFlipping = false; });
                        _notebookAudioRef.PlaySound(_notebookAudioRef.pageTurn);
                    }
                    else
                    {
                        _flippingStarted = false;
                        _notebookRef.interactable = _isBookInteractable;
                        this.enabled = false;
                    }
                    _nextPageCountDown = _pageFlipTime + _timeBetweenPages + Time.deltaTime;
                }
                _nextPageCountDown -= Time.deltaTime;
            }
        }
    }
}
