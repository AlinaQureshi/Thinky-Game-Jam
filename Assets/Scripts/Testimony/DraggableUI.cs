using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] TestimonyItem _testimonyItem;
    [SerializeField] TestimonyType _testimonyType;
    [SerializeField] float _scaleMultiplier = 1.2f;
    [SerializeField] float _resetTime;

    [SerializeField] Vector3 _offset = Vector3.zero;

    [SerializeField] Image _image;
    [SerializeField] GameObject _highlightObject;

    Transform _canvaParent;
    Transform _testimonyParent;

    private GameObject _collisionUI;

    private bool _isDiscovered;
    private bool _isDragging;
    private bool _isAtPlace;

    private Vector3 _originalPosition;
    private Vector3 _localPosition;
    private Vector3 _currentScale;


    private void Start()
    {
        _originalPosition = this.transform.position;
        _localPosition = this.transform.localPosition;
        _canvaParent = TestimonyManager.Instance.GetCanvaTransform();
        _testimonyParent = TestimonyManager.Instance.GetTestimonyTransform();
    }

    public TestimonyItem GetTestimonyItem() { return _testimonyItem; }
    public void ItemDiscovered() {
        _isDiscovered = true;
        if (_highlightObject) _highlightObject.SetActive(false);
        if (_image) _image.raycastTarget = false;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isDiscovered) return;

        _isDragging = true;
        _isAtPlace = false;
        if (_testimonyType == TestimonyType.Writing)
        {
            transform.SetParent(_canvaParent);
            transform.position = _originalPosition;
        }   
        else
        {
            transform.localScale = Vector3.one * _scaleMultiplier;
        }
        TestimonyManager.Instance.UpdateCanvaOrder(_testimonyType);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDiscovered) return;
        var currentPos = GetMousePos() + _offset; 
        transform.position = new Vector3 (currentPos.x, currentPos.y, 0);
    }

    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isDiscovered) return;

        _isDragging = false;
        if (_testimonyType == TestimonyType.Writing) { transform.SetParent(_testimonyParent); }
        else { transform.localScale = Vector3.one; }
        CheckCollision();
    }

    void CheckCollision()
    {
        if (_collisionUI)      
        {
            var colliderScript = _collisionUI.GetComponent<DraggableUI>();
            TestimonyManager.Instance.CheckContradiction(this, colliderScript);
        }
        _collisionUI = null;

        if (_testimonyType == TestimonyType.Sketch) { ResetBackToPosition(); }
        else { ResetBackToLocalPosition(); }
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
        if (_isDragging && !_isAtPlace) { _collisionUI = collision.gameObject; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_collisionUI == collision.gameObject) { _collisionUI = null; }

    }
}
