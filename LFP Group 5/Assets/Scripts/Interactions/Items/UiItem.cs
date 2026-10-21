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
    [SerializeField] private GameObject _lockImage = null;
    private Sprite _uiSprite = null;
    private string _itemName = "";
    private string _itemDescription = "";
    private bool _hasItem = false;
    private Item _currentItem = null;
    public event Action<UiItem> OnItemClicked;

    private GameMode _currentMode = GameMode.NormalMode;

    private Image _backGroundImage = null;
    private Color _originalColour;

    void OnDisable()
    {
        if(_currentItem != null)
        {
            _currentItem.OnItemLock -= SetItemLock;
        }       
    }

    public bool GetHasItem()
    {
        return _hasItem;
    }
    public void SetItem(Item newItem)
    {
        if(_currentItem != null)
        {
            _currentItem.OnItemLock -= SetItemLock;
        }

        _currentItem = newItem;

        if(_currentItem != null)
        {
            _currentItem.OnItemLock += SetItemLock;
        }

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

            if(_currentMode == GameMode.NormalMode)
            _uiSprite = _currentItem.GetNormalModeUISprite();
            else
            _uiSprite = _currentItem.GetSafeModeUISprite();


            _hasItem = true;
        }
        _image.sprite = _uiSprite;
        _image.preserveAspect = true;
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
    public void DeSelectItem()
    {
        if(_currentItem == null) return;
        _currentItem.SetIsSelected(false);
        if(_backGroundImage != null)
        _backGroundImage.color = _originalColour;
    }
    public void SelectItem()
    {
        if(_currentItem == null) return;
        _currentItem.SetIsSelected(true);
        if(_backGroundImage != null)
        _backGroundImage.color = Color.yellowGreen;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if(_currentItem == null) return;
        if(!_currentItem.GetIsActive()) return;

        if(_currentItem.GetIsSelected())
        {
            DeSelectItem();
            OnItemClicked.Invoke(null);
            return;
        }

        OnItemClicked?.Invoke(this);
    }

    private void SetItemLock(bool newValue)
    {
        if(!newValue) DeSelectItem();
        if(_lockImage == null) return;
        _lockImage.SetActive(!newValue);
    }

    public void SetNormalMode()
    {
        _currentMode = GameMode.NormalMode;
        SetNormalModeUISprite();
    }
    public void SetSafeMode()
    {
        _currentMode = GameMode.SafeMode;
        SetSafeModeUISprite();
    }
    public void SetNormalModeUISprite()
    {
        if(_currentItem == null) return;
        _image.sprite = _currentItem.GetNormalModeUISprite();
        _image.preserveAspect = true;
    }

    public void SetSafeModeUISprite()
    {
        if(_currentItem == null) return;
        _image.sprite = _currentItem.GetSafeModeUISprite();
        _image.preserveAspect = true;
    }

    public void SetBackGroundImage(Image newImage)
    {
        _backGroundImage = newImage;
        _originalColour = _backGroundImage.color;
    }
}
