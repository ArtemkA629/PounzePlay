using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static readonly float _deltaXForBound = 0.6f;

    [SerializeField] private Camera _camera;
    [SerializeField] private Bowl _bowl;
    [SerializeField] private Vector3 direction;

    private float _cameraBoundX;
    private bool _isMoving;

    private void Start()
    {
        _cameraBoundX = _camera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - _deltaXForBound;
    }

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

        _bowl.transform.position = new Vector3(Mathf.Clamp(_bowl.transform.position.x, -_cameraBoundX, _cameraBoundX),
            _bowl.transform.position.y, _bowl.transform.position.z);
    }
}
