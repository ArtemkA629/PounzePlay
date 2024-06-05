using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelProcess : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _loadingPercentText;
    [SerializeField] private Webview _webview;
    [SerializeField] private GameObject _onboarding1;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _totalScoreView;

    private void Start()
    {
        if (GameSettings.PlayerEntered == false)
            GameSettings.PlayerEntered = true;
        else
            GoToMenu();
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (_slider.value == 1f)
        {
            if (_webview.TryOnOnboarding() == false && _webview.gameObject.activeInHierarchy)
                return;
            else if (GameSettings.OnBoardingShowed)
                GoToMenu();
            else
                _onboarding1.SetActive(true);
            gameObject.SetActive(false);
            return;
        }

        _slider.value = Mathf.Lerp(_slider.value, 1f, 0.5f);
        _loadingPercentText.text = Convert.ToInt32(_slider.value * 100).ToString("#,0", CultureInfo.InvariantCulture) + "%";
    }

    private void GoToMenu()
    {
        _mainMenu.SetActive(true);
        _background.SetActive(true);
        _totalScoreView.SetActive(true);
        gameObject.SetActive(false);
    }
}
