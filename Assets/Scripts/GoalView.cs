using TMPro;
using UnityEngine;

public class GoalView : MonoBehaviour
{
    [SerializeField] private GameLoading _gameLoading;
    [SerializeField] private TextMeshProUGUI _goalText;

    private void Start()
    {
        _gameLoading.Goal.Collected += OnCollected;
    }

    private void OnDisable()
    {
        _gameLoading.Goal.Collected -= OnCollected;
    }

    private void OnCollected(int newCount)
    {
        _goalText.text = newCount.ToString();
    }
}
