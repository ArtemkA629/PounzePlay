using UnityEngine;

[CreateAssetMenu(fileName = "DisabledUI", menuName = "ScriptableObjects/DisabledUI")]
public class DisabledUI : ScriptableObject
{
    [SerializeField] private Color _butonColor;
    [SerializeField] private Color _butonTextColor;
    
    public Color ButonColor => _butonColor;
    public Color ButonTextColor => _butonTextColor;
}
