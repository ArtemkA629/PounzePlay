using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SettingsToogleController : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _toggleImage;
    [SerializeField] private Sprite _offToggleSprite;
    [SerializeField] private Sprite _onToggleSprite;

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnSwitch);
        if (_toggle.isOn)
            OnSwitch(true);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }

    public void OnSwitch(bool isOn)
    {
        _toggleImage.sprite = isOn ? _onToggleSprite : _offToggleSprite;
    }
}
