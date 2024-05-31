using UnityEngine;

public class Bowl : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _borderX;

    public float Speed => _speed;
    public float BorderX => _borderX;
}
