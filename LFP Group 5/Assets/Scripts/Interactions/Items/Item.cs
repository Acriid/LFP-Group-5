using Unity.VisualScripting;
using UnityEngine;

public class Item : Interaction
{
    [SerializeField] private ItemSO _itemSO = null;
    public override void Interact(GameObject interactingObject)
    {
        if(!_canInteract) return;
        if (_itemSO == null) return;

        PickUpItem(interactingObject);
    }
    private void PickUpItem(GameObject interactingObject)
    {
        
    }

    public ItemSO GetItemSO()
    {
        return _itemSO;
    }

    public bool GetIsActive()
    {
        return _itemSO.IsActive;
    }
    public bool GetIsCorrupt()
    {
        return _itemSO.IsCorrupt;
    }
    public bool GetIsSelected()
    {
        return _itemSO.IsSelected;
    }

    public string GetItemName()
    {
        return _itemSO.ItemName;
    }
    public string GetItemDescription()
    {
        return _itemSO.ItemDescription;
    }
    public Sprite GetItemSprite()
    {
        return _itemSO.UISprite;
    }

    public void SetIsCorrupt(bool newValue)
    {
        _itemSO.IsCorrupt = newValue;
    }
    public void SetIsActive(bool newValue)
    {
        _itemSO.IsActive = newValue;
    }
    public void SetIsSelected(bool newValue)
    {
        _itemSO.IsSelected = newValue;
    }
}
