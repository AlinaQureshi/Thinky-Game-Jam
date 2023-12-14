using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] TestimonyItem _testimonyItem;
    [SerializeField] TestimonyType _testimonyType;
    [SerializeField] float _resetTime;

    private GameObject _collisionUI;

    private bool _isDiscovered;
    private bool _isDragging;
    private bool _isAtPlace;

    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = this.transform.position;
    }

    public TestimonyItem GetTestimonyItem() { return _testimonyItem; }
    public void ItemDiscovered() { _isDiscovered = true; }


    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
        _isAtPlace = false;
        TestimonyManager.Instance.UpdateCanvaOrder(_testimonyType);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDiscovered) return;
        var currentPos = GetMousePos();
        transform.position = new Vector3 (currentPos.x, currentPos.y, 0);
    }

    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        CheckCollision();
    }

    void CheckCollision()
    {
        if (_collisionUI)      
        {
            var colliderScript = _collisionUI.GetComponent<DraggableUI>(); ;
            TestimonyManager.Instance.CheckContradiction(this, colliderScript);
        }
        _collisionUI = null;
        ResetBackToPosition();
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
