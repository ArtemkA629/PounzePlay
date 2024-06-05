using System.Collections;
using UnityEngine;

public class BallsSpawner : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _ballPrefab;
    [SerializeField] private Sprite[] _ballSprites;
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _spawnLeftX;
    [SerializeField] private float _spawnRightX;
    [SerializeField] private float _spawnUpperY;
    [SerializeField] private float _spawnLowerY;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            var spawnPosition = new Vector2(
                Random.Range(_spawnLeftX, _spawnRightX),
                Random.Range(_spawnLowerY, _spawnUpperY)
            );

            Instantiate(_ballPrefab, spawnPosition, Quaternion.identity, transform);
            _ballPrefab.sprite = _ballSprites[Random.Range(0, _ballSprites.Length - 1)];
        }
    }
}
