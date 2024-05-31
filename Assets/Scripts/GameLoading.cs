using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLoading : MonoBehaviour
{
    [SerializeField] private Sprite _ballSprite;
    [SerializeField] private Image _ballImage;
    [SerializeField] private TextMeshProUGUI _goalCountText;
    [SerializeField] private int _goalCount;

    private Goal _goal;

    public Goal Goal => _goal;

    private void Awake()
    {
        _goal = new Goal(_ballSprite, _goalCount);
        _ballImage.sprite = _ballSprite;
        _goalCountText.text = _goalCount.ToString();
        GameSettings.GoalSprite = _ballSprite;
    }
}
