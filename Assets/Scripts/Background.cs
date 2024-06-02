using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private int _scoreToBuy;
    [SerializeField] private Button _backgroundButton;
    [SerializeField] private Image _backgroundButtonImage;
    [SerializeField] private Image _backgroundCardImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TextMeshProUGUI _backgroundButtonText;
    [SerializeField] private DisabledUI _disabledButton;
    [SerializeField] private Sprite _defaultBackgroundSprite;
    [SerializeField] private Sprite _activeBackgroundSprite;
    [SerializeField] private Sprite _mainBackground;

    private bool _bought;

    public Image BackgroundCardImage => _backgroundCardImage;
    public Image BackgroundImage => _backgroundImage;
    public TextMeshProUGUI BackgroundButtonText => _backgroundButtonText;
    public int ScoreToBuy => _scoreToBuy;
    public bool Bought => _bought;

    private void Start()
    {
        if (GameSettings.CurrentBackground.name == name)
        {
            _backgroundCardImage.sprite = _activeBackgroundSprite;
            _backgroundButtonText.text = BackgroundConstants.Chosen;
            _bought = true;
        }

        foreach (var background in GameSettings.AvailiableBackgrounds)
            if (name == background.name)
            {
                _backgroundButtonText.text = BackgroundConstants.Choose;
                _bought = true;
                break;
            }

        if (_bought == false && GameSettings.TotalScore < _scoreToBuy)
        {
            _backgroundButton.enabled = false;
            _backgroundButtonImage.color = _disabledButton.ButonColor;
            _backgroundButtonText.color = _disabledButton.ButonTextColor;
        }
    }

    public void Activate()
    {
        GameSettings.CurrentBackground.BackgroundCardImage.sprite = _defaultBackgroundSprite;
        GameSettings.CurrentBackground.BackgroundButtonText.text = BackgroundConstants.Choose;
        _backgroundCardImage.sprite = _defaultBackgroundSprite;
        _backgroundButtonText.text = BackgroundConstants.Chosen;

        if (_bought == false)
        {
            GameSettings.SetTotalScore(GameSettings.TotalScore - _scoreToBuy);
            _bought = true;
        }

        _backgroundImage.sprite = _mainBackground;
        SaveSystem.Save();
    }
}
