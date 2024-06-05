using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private Background[] _otherBackgrounds;
    [SerializeField] private Button _backgroundButton;
    [SerializeField] private Image _backgroundButtonImage;
    [SerializeField] private Image _backgroundCardImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TextMeshProUGUI _backgroundButtonText;
    [SerializeField] private BackgroundInfo _info;

    private bool _bought;

    public Image BackgroundCardImage => _backgroundCardImage;
    public Image BackgroundImage => _backgroundImage;
    public TextMeshProUGUI BackgroundButtonText => _backgroundButtonText;
    public BackgroundInfo Info => _info;
    public bool Bought => _bought;

    private void Start()
    {
        if (GameSettings.CurrentBackground.Name == name)
        {
            _backgroundCardImage.sprite = _info.ActiveBackgroundSprite;
            _backgroundButtonText.text = BackgroundConstants.Chosen;
            _bought = true;
        }

        foreach (var background in GameSettings.AvailiableBackgrounds)
            if (name == background.Name)
            {
                _backgroundButtonText.text = BackgroundConstants.Choose;
                _bought = true;
                break;
            }

        if (_bought)
            return;

        if (GameSettings.TotalScore < _info.ScoreToBuy)
        {
            _backgroundButton.enabled = false;
            _backgroundButtonImage.color = _info.DisabledButton.ButonColor;
            _backgroundButtonText.color = _info.DisabledButton.ButonTextColor;
        }
        _backgroundButtonText.text = _info.ScoreToBuy.ToString("#,0", CultureInfo.InvariantCulture) + " SCORE";
    }

    public void Activate()
    {
        if (_backgroundButtonText.text == BackgroundConstants.Chosen)
            return;

        foreach (var background in _otherBackgrounds)
            if (background.name == GameSettings.CurrentBackground.Name)
            {
                background.BackgroundCardImage.sprite = background._info.DefaultBackgroundSprite;
                background.BackgroundButtonText.text = BackgroundConstants.Choose;
                break;
            }
        GameSettings.CurrentBackground = _info;
        _backgroundCardImage.sprite = _info.ActiveBackgroundSprite;
        _backgroundButtonText.text = BackgroundConstants.Chosen;

        if (_bought == false)
        {
            GameSettings.SetTotalScore(GameSettings.TotalScore - _info.ScoreToBuy);
            GameSettings.AvailiableBackgrounds.Add(_info);
            _bought = true;
        }

        _backgroundImage.sprite = _info.MainBackground;
        SaveSystem.Save();
    }
}
