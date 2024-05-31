using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _positionYToCount;

    private Goal _goal;
    private GameResult _gameOver;

    private void Start()
    {
        _goal = FindObjectOfType<GameLoading>().Goal;
        var gameResults = FindObjectsOfType<GameResult>();
        foreach (var gameResult in gameResults )
            if (gameResult.Win == false)
            {
                _gameOver = gameResult;
                break;
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.y > _positionYToCount && collision.TryGetComponent(out Bowl bowl))
            if (_spriteRenderer.sprite == GameSettings.GoalSprite)
            {
                _goal.SubtractCount();
                Destroy(gameObject);
            }
            else
                _gameOver.Apply();
    }
}
