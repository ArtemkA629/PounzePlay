using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccessBowlInfoView : MonoBehaviour
{
    [SerializeField] private SnapScroll _snapScroll;

    [Header("UIElements")]
    [SerializeField] private TextMeshProUGUI _accessText;
    [SerializeField] private TextMeshProUGUI _startButtonText;
    [SerializeField] private Button _button;

    [Header("DisabledButtonColors")]
    [SerializeField] private DisabledUI _disabledButton;

    private void OnEnable()
    {
        _snapScroll.CardChanged += OnCardChanged;
    }

    private void OnDisable()
    {
        _snapScroll.CardChanged -= OnCardChanged;
    }

    private void OnCardChanged(BowlCard card)
    {
        if (GameSettings.TotalScore >= card.Score)
        {
            UpdateView(false, Color.white, Color.white, true);
            GameSettings.CurrentBowlCard = card;
        }
        else
        {
            _accessText.text = "Will open at " + card.Score.ToString("#,0", CultureInfo.InvariantCulture) + " score";
            UpdateView(true, _disabledButton.ButonColor, _disabledButton.ButonTextColor, false);
        }
    }

    private void UpdateView(bool textVisible, Color buttonColor, Color buttonTextColor, bool buttonEnabled)
    {
        _accessText.gameObject.SetActive(textVisible);
        _button.GetComponent<Image>().color = buttonColor;
        _startButtonText.color = buttonTextColor;
        _button.enabled = buttonEnabled;
    }
}
