using System;
using TMPro;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    [SerializeField] private GameLoading _gameLoading;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private GameObject _rightButton;
    [SerializeField] private GameObject _resultText;
    [SerializeField] private TextMeshProUGUI _achievedText;
    [SerializeField] private TextMeshProUGUI _totalScoreText;
    [SerializeField] private bool _win;

    public bool Win => _win;

    private void OnDisable()
    {
        _gameLoading.Goal.Won -= OnWin;
    }

    public void SubscribeEvents()
    {
        _gameLoading.Goal.Won += OnWin;
    }

    public void Apply()
    {
        Time.timeScale = 0f;
        ConfigureResultPanel();
    }

    private void ConfigureResultPanel()
    {
        var goal = _gameLoading.Goal;
        int achieved = (goal.CountAtStart - goal.Count) * 2;
        int totalScore = GameSettings.TotalScore + achieved;
        if (achieved < 0 || totalScore < 0)
            throw new Exception("Invalid win count");

        GameSettings.SetTotalScore(totalScore);
        SaveSystem.Save();
        UpdateView(achieved);
        _resultPanel.SetActive(true);
    }

    private void UpdateView(int achieved)
    {
        _achievedText.text = "+ " + achieved.ToString();
        _totalScoreText.text = "Total score: " + GameSettings.TotalScore.ToString();
        _resultText.SetActive(true);
        _rightButton.SetActive(true);
    }

    private void OnWin()
    {
        if (_win)
        {
            Apply();
            GameSettings.SetGoal(_gameLoading.Goal.CountAtStart + GoalConstants.AddingCount);
            SaveSystem.Save();
        }
    }
}
