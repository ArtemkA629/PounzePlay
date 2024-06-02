using System.Globalization;
using TMPro;
using UnityEngine;

public class TotalScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalScoreCountText;

    private void Start()
    {
        _totalScoreCountText.text = GameSettings.TotalScore.ToString("#,0", CultureInfo.InvariantCulture);
    }
}
