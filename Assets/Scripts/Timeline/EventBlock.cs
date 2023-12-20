using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] float _resetTime;
    [SerializeField] int _eventOrder;
    [SerializeField] TMP_Text _eventText;

    private GameObject _collisionUI;


    private bool _isDiscovered;
    private bool _isDragging;
    private bool _isAtPlace;

    private Vector3 _originalPosition;
    private Vector3 _localPosition;


    private void Start()
    {
        _originalPosition = this.transform.position;
        _localPosition = this.transform.localPosition;
    }

    public void ItemDiscovered()
    {
        _isDiscovered = true;
    }

    public int GetOrder()
    {
        return _eventOrder;
    }


    public void UpdateEvent(string eventText, int eventOrder)
    {
        _eventText.text = eventText;
        _eventOrder = eventOrder;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isDiscovered) return;
        _isDragging = true;
        _isAtPlace = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDiscovered) return;
        var currentPos = GetMousePos();
        transform.position =  new Vector3(currentPos.x, currentPos.y, _originalPosition.z);
    }

    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isDiscovered) return;
        _isDragging = false;
        CheckCollision();
    }


    void CheckCollision()
    {
        if (_collisionUI)
        {
            var colliderScript = _collisionUI.GetComponent<EventMatch>();
            TimelineManager.Instance.ConnectEvents(this, colliderScript);
        }
        else
        {
            ResetBackToLocalPosition();
        }
        _collisionUI = null;
    }

    public void ResetBackToLocalPosition()
    {
        TimelineManager.Instance.ResetDictKey(this);
        Vector3 startingPos = transform.localPosition;
        float elapsedTime = 0;
        StartCoroutine(SmoothMovement());
        IEnumerator SmoothMovement()
        {
            while (elapsedTime < _resetTime)
            {
                transform.localPosition = Vector3.Lerp(startingPos, _localPosition, (elapsedTime / _resetTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = _localPosition;
            _isAtPlace = true;
        }
    }

    public void SnapToPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDiscovered || this.CompareTag(collision.tag)) return;
        if (_isDragging && !_isAtPlace) {
            if (collision.tag == "EventMatch")
            {
                _collisionUI = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (_collisionUI == collision.gameObject) { 
            _collisionUI = null;
        }
    }
}
