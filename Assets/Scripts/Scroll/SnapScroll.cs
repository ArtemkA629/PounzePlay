using System;
using UnityEngine;
using UnityEngine.UI;

public class SnapScroll : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollBar;
    [SerializeField] private float _xScaleRatio;
    [SerializeField] private float _yScaleRatio;

    private float _scrollPosition;
    private float[] _positions;
    private BowlCard _currentBowlCard;

    public event Action<BowlCard> CardChanged;

    private void Update()
    {
        _positions = new float[transform.childCount];
        float distance = 1f / (_positions.Length - 1f);
        for (int i = 0; i < _positions.Length; i++)
            _positions[i] = distance * i;

        _scrollPosition = _scrollBar.value;
        if (Input.GetMouseButton(0) == false)
        {
            for (int i = 0; i < _positions.Length; i++)
                if (CanSnap(i, distance))
                    _scrollBar.value = Mathf.Lerp(_scrollBar.value, _positions[i], 0.1f);
        }

        for (int i = 0; i < _positions.Length; i++)
            if (CanSnap(i, distance))
            {
                ChangeScale(i, _xScaleRatio, _yScaleRatio, out BowlCard bowlCard, true);
                for (int j = 0; j < _positions.Length; j++)
                    if (j != i)
                        ChangeScale(j, 1f, 1f, out _, false);
                    else if (_currentBowlCard == null || _currentBowlCard.Type != bowlCard.Type)
                    {
                        _currentBowlCard = bowlCard;
                        CardChanged?.Invoke(bowlCard);
                    }
            }
    }

    private bool CanSnap(int number, float distance)
    {
        return _scrollPosition < _positions[number] + (distance / 2) && _scrollPosition > _positions[number] - (distance / 2);
    }

    private void ChangeScale(int number, float xRatio, float yRatio, out BowlCard bowlImage, bool setActive)
    {
        var scalableImage = transform.GetChild(number).GetChild(0);
        scalableImage.localScale =
            Vector2.Lerp(scalableImage.localScale, new Vector2(xRatio, yRatio), 0.1f);
        bowlImage = transform.GetChild(number).GetComponent<BowlCard>();
        scalableImage.gameObject.SetActive(setActive);
    }
}
