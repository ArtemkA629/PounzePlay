using System;
using UnityEngine;

[Serializable]
public class BackgroundInfo
{
    [SerializeField] private int _scoreToBuy;
    [SerializeField] private string _name;
    [SerializeField] private DisabledUI _disabledButton;
    [SerializeField] private Sprite _defaultBackgroundSprite;
    [SerializeField] private Sprite _activeBackgroundSprite;
    [SerializeField] private Sprite _mainBackground;

    public int ScoreToBuy => _scoreToBuy;
    public DisabledUI DisabledButton => _disabledButton;
    public Sprite DefaultBackgroundSprite => _defaultBackgroundSprite;
    public Sprite ActiveBackgroundSprite => _activeBackgroundSprite;
    public Sprite MainBackground => _mainBackground;
    public string Name => _name;
}
