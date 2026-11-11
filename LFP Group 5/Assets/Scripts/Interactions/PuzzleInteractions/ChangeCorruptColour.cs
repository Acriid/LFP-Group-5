using UnityEngine;

public class ChangeCorruptColour : MonoBehaviour
{
    public GameObject _corruptItem;
    //public SpriteRenderer[] _spriteRenderers;

    public SpriteRenderer _normalSprite;
    public SpriteRenderer _corruptedSprite;

    private void Awake()
    {
        //_spriteRenderers = _corruptItem.GetComponentsInChildren<SpriteRenderer>();
        //ChangeColor(Color.red);

        _normalSprite.enabled = true;
        _corruptedSprite.enabled = false;
    }

    //private void ChangeColor(Color newColor)
    //{
        //foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        //{
            //spriteRenderer.color = newColor;
        //}
   // }

    private void Start()
    {
        _normalSprite.enabled = false;
        _corruptedSprite.enabled = true;

        //ChangeColor(Color.red);
    }
}
