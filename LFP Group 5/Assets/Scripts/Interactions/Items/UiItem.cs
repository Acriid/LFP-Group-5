using System;
using TMPro;
using UnityEngine;

public class UiItem : MonoBehaviour
{
    //The GameObject requires :
    //Secret image(0 Alpha) over the SpriteRenderer to use the hover code
    //Panel as a child to use the hover code.
    //Text on the child panel for the name and description.
    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    [SerializeField] private TMP_Text _itemNameText = null;
    [SerializeField] private TMP_Text _itemDescriptionText = null;
    private Sprite _uiSprite = null;
    private string _itemName = "";
    private string _itemDescription = "";
    private bool _hasItem = false;
    private Item _currentItem = null;
    public bool GetHasItem()
    {
        return _hasItem;
    }
    public void SetItem(Item newItem)
    {
        _currentItem = newItem;
        SetItemValues();
    }
    private void SetItemValues()
    {
        if(_currentItem == null)
        {
            _itemName = "";
            _itemDescription = "";
            _uiSprite = null;
            _hasItem = false;
        }
        else
        {
            _itemName = _currentItem.GetItemName();
            _itemDescription = _currentItem.GetItemDescription();
            _uiSprite = _currentItem.GetItemSprite();
            _hasItem = true;
        }
        _spriteRenderer.sprite = _uiSprite;
        _itemDescriptionText.text = _itemDescription;
        _itemNameText.text = _itemName;
    }

    public Item GetItem()
    {
        return _currentItem;
    }
}
