using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(ItemDragHandler))]
public class UiItem : MonoBehaviour, IPointerClickHandler
{
    //The GameObject requires :
    //Panel as a child to use the hover code.
    //Text on the child panel for the name and description.
    [SerializeField] private Image _image = null;
    [SerializeField] private TMP_Text _itemNameText = null;
    [SerializeField] private TMP_Text _itemDescriptionText = null;
    private Sprite _uiSprite = null;
    private string _itemName = "";
    private string _itemDescription = "";
    private bool _hasItem = false;
    private Item _currentItem = null;
    public event Action<UiItem> OnItemClicked;
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
        _image.sprite = _uiSprite;
        _itemDescriptionText.text = _itemDescription;
        _itemNameText.text = _itemName;
    }

    public Item GetItem()
    {
        return _currentItem;
    }
    public ItemDragHandler GetItemDragHandler()
    {
        return GetComponent<ItemDragHandler>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnItemClicked?.Invoke(this);
    }
}
