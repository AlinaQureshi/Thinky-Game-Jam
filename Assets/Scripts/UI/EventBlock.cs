using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] float _resetTime;
    [SerializeField] int _eventOrder;

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


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isDiscovered) return;
        Debug.Log("OnBeginDrag");

        _isDragging = true;
        _isAtPlace = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        if (_isDiscovered) return;
        var currentPos = GetMousePos();
        transform.position = new Vector3(currentPos.x, currentPos.y, 0);
    }

    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isDiscovered) return;
        ResetBackToLocalPosition();
        _isDragging = false;
        //CheckCollision();
    }

    void ResetBackToPosition()
    {
        Vector3 startingPos = transform.position;
        float elapsedTime = 0;
        StartCoroutine(SmoothMovement());
        IEnumerator SmoothMovement()
        {
            while (elapsedTime < _resetTime)
            {
                transform.position = Vector3.Lerp(startingPos, _originalPosition, (elapsedTime / _resetTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = _originalPosition;
            _isAtPlace = true;
        }
    }
    void ResetBackToLocalPosition()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDiscovered || this.CompareTag(collision.tag)) return;
        //if (_isDragging && !_isAtPlace) { _collisionUI = collision.gameObject; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       // if (_collisionUI == collision.gameObject) { _collisionUI = null; }
    }
}
