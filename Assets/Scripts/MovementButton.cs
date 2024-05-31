using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Bowl _bowl;
    [SerializeField] private Vector3 direction;

    private bool _isMoving;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isMoving = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isMoving = false;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
            _bowl.transform.position += _bowl.Speed * Time.deltaTime * direction;

        _bowl.transform.position = new Vector3(Mathf.Clamp(_bowl.transform.position.x, -_bowl.BorderX, _bowl.BorderX),
            _bowl.transform.position.y, _bowl.transform.position.z);
    }
}
