using System.Globalization;
using TMPro;
using UnityEngine;

public class TotalScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalScoreCountText;

    private void OnEnable()
    {
        GameSettings.OnScoreChanged += ChangeScoreView;
    }

    private void OnDisable()
    {
        GameSettings.OnScoreChanged -= ChangeScoreView;
    }

    private void Start()
    {
        ChangeScoreView();
    }

    private void ChangeScoreView()
    {
        _totalScoreCountText.text = GameSettings.TotalScore.ToString("#,0", CultureInfo.InvariantCulture);
    }
}
