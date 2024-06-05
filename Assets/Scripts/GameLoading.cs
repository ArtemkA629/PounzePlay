using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLoading : MonoBehaviour
{
    [SerializeField] private Sprite[] _ballSprites;
    [SerializeField] private Image _ballImage;
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private SpriteRenderer _floor;
    [SerializeField] private SpriteRenderer _ceiling;
    [SerializeField] private SpriteRenderer _bowl;
    [SerializeField] private TextMeshProUGUI _goalCountText;
    [SerializeField] private GameResult _win;

    private Goal _goal;

    public Goal Goal => _goal;

    private void OnEnable()
    {
        var ballSprite = _ballSprites[Random.Range(0, _ballSprites.Length - 1)];
        _ballImage.sprite = ballSprite;
        GameSettings.GoalSprite = ballSprite;

        SetGoal(GameSettings.GoalCount);
        SetBowl(GameSettings.CurrentBowlCard);
        _background.sprite = GameSettings.CurrentBackground.MainBackground;

        _win.SubscribeEvents();
    }

    private void SetGoal(int goalCount)
    {
        _goal = new Goal(GameSettings.GoalSprite, goalCount);
        _goalCountText.text = goalCount.ToString();
    }

    private void SetBowl(BowlCard currentBowlCard)
    {
        _floor.color = currentBowlCard.EnvironmentColor;
        _ceiling.color = currentBowlCard.EnvironmentColor;
        _bowl.sprite = currentBowlCard.Sprite;
    }
}
