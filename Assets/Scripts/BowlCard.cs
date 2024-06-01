using UnityEngine;

public class BowlCard : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private BowlType _type;

    public int Score => _score;
    public BowlType Type => _type;
}
