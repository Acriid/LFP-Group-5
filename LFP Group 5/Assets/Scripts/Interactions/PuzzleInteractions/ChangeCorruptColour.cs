using UnityEngine;

public class ChangeCorruptColour : MonoBehaviour
{
    public GameObject _corruptItem;
    public SpriteRenderer[] _spriteRenderers;
    private void Awake()
    {
        _spriteRenderers = _corruptItem.GetComponentsInChildren<SpriteRenderer>();
        ChangeColor(Color.red);

    }

    private void ChangeColor(Color newColor)
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = newColor;
        }
    }
}
