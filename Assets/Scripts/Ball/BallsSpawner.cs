using System.Collections;
using UnityEngine;

public class BallsSpawner : MonoBehaviour
{
    private readonly static float _deltaXForBound = 0.6f;

    [SerializeField] private Camera _camera;
    [SerializeField] private SpriteRenderer _ballPrefab;
    [SerializeField] private Sprite[] _ballSprites;
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _spawnUpperY;
    [SerializeField] private float _spawnLowerY;

    private float _cameraBoundX;

    private void Start()
    {
        StartCoroutine(Spawn());
        _cameraBoundX = _camera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - _deltaXForBound;
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            var spawnPosition = new Vector2(
                Random.Range(-_cameraBoundX, _cameraBoundX),
                Random.Range(_spawnLowerY, _spawnUpperY)
            );

            Instantiate(_ballPrefab, spawnPosition, Quaternion.identity, transform);
            _ballPrefab.sprite = _ballSprites[Random.Range(0, _ballSprites.Length - 1)];
        }
    }
}
