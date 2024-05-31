using UnityEngine;
using UnityEngine.UI;

public class ControllingScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private float _elementScaleFactor = 1.2f;

    private float[] _elementDistances;
    private bool _dragging;
    private int _closestElementIndex = 0;
    private int _contentChildCount;

    private void Start()
    {
        _contentChildCount = _scrollRect.content.childCount;
        _elementDistances = new float[_contentChildCount];
    }

    private void Update()
    {
        if (!_dragging)
        {
            FindClosestElement();
            LerpToElement(_closestElementIndex);
        }
    }

    private void FindClosestElement()
    {
        float distance = Mathf.Infinity;
        Vector2 currentLocation = _scrollRect.content.anchoredPosition;

        for (int i = 0; i < _contentChildCount; i++)
        {
            float dist = Mathf.Abs(_scrollRect.content.GetChild(i).GetComponent<RectTransform>().anchoredPosition.x - currentLocation.x);

            if (dist < distance)
            {
                distance = dist;
                _closestElementIndex = i;
            }
        }
    }

    private void LerpToElement(int index)
    {
        float targetX = _scrollRect.content.GetChild(index).GetComponent<RectTransform>().anchoredPosition.x;
        Vector2 targetLocation = new Vector2(targetX, _scrollRect.content.anchoredPosition.y);
        _scrollRect.content.anchoredPosition = Vector2.Lerp(_scrollRect.content.anchoredPosition, targetLocation, 10 * Time.deltaTime);

        for (int i = 0; i < _contentChildCount; i++)
        {
            var child = _scrollRect.content.GetChild(i);
            if (i == index)
                child.transform.localScale = new Vector3(_elementScaleFactor, _elementScaleFactor, 1f);
            else
                child.transform.localScale = Vector3.one;
        }
    }

    public void OnDrag()
    {
        _dragging = true;
    }

    public void OnEndDrag()
    {
        _dragging = false;
    }
}
