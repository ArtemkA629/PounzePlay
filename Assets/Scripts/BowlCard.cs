using UnityEngine;

public class BowlCard : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Sprite _standartCardSprite;
    [SerializeField] private Sprite _spriteCardWithBorders;
    [SerializeField] private Color _environmentColor;

    public int Score => _score;
    public Sprite Sprite => _sprite;
    public Sprite StandartCardSprite => _standartCardSprite;
    public Sprite SpriteCardWithBorders => _spriteCardWithBorders;
    public Color EnvironmentColor => _environmentColor;
}
