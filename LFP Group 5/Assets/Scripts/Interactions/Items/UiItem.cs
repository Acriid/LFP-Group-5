using System;
using UnityEngine;

public class UiItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    private Sprite _uiSprite = null;
    private string _itemName = "";
    private string _itemDescription = "";
    private bool _isInitialized = false;
    private Item _currentItem = null;
    public void Initialize(Item item)
    {
        
    }
    public bool GetIsInitialized()
    {
        return _isInitialized;
    }
    public void SetItem(Item newItem)
    {
        _currentItem = newItem;
    }
    private void SetItem()
    {
        if(_currentItem == null)
        {
            _itemName = "";
            _itemDescription = "";
            _uiSprite = null;
        }
        else
        {
            _itemName = _currentItem.GetItemName();
            _itemDescription = _currentItem.GetItemDescription();
            _uiSprite = _currentItem.GetItemSprite();
        }
    }

    public Item GetItem()
    {
        return _currentItem;
    }

    private void SetItemValues()
    {
        
    }
}
