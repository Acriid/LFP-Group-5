using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> _itemList;
    //When max items gets smaller items drop from back of the list.
    [SerializeField] private int _maxItems = 4;
    private int _currentItems = 0;
    public event Action<Item> OnItemPickup;
    public event Action<Item> OnItemRemove;
    //Should inventory keep track of the current item?

    void OnEnable()
    {
        _itemList = new(_maxItems);
    }

    public void ChangeMaxItems(int newValue)
    {
        _maxItems = newValue;

        //TODO- Change inventory to lock items
    }

    public void AddToInventoryItem(GameObject itemToAdd)
    {
        if(_currentItems == _maxItems) return;
        if(!CheckIfItem(itemToAdd,out Item itemComponent,false,true))return;
        if(_itemList.Contains(itemComponent)) return;

        _itemList.Add(itemComponent);
        _currentItems++;
        OnItemPickup?.Invoke(itemComponent);
    }
    public void RemoveItemFromInventory(GameObject itemToRemove)
    {
        if(!CheckIfItem(itemToRemove,out Item itemComponent,true))return;
        Debug.Log("Item");
        if(!_itemList.Contains(itemComponent)) return;

        Debug.Log("Removed");
        _itemList.Remove(itemComponent);
        _currentItems--;
        OnItemRemove?.Invoke(itemComponent);
    }
    public void ChangeItemSlot(GameObject itemToChange, int newSlot)
    {
        if (newSlot < 0 || newSlot >= _maxItems) return;
        if(CheckIfItem(itemToChange,out Item itemComponent,true))return;

        int originalSlot = _itemList.IndexOf(itemComponent);
        if (originalSlot < 0) return;

        (_itemList[originalSlot], _itemList[newSlot]) = (_itemList[newSlot], _itemList[originalSlot]);
    }

    private bool CheckIfItem(GameObject objectToCheck, out Item itemComponent, bool checkActive = false, bool checkCorrupt = false)
    {

        if(objectToCheck.TryGetComponent(out UiItem uiItemComponent))
        {
            itemComponent = uiItemComponent.GetItem();
            if(itemComponent == null) return false;
        }
        else
        {
            //If there is no item returns false
            if(!objectToCheck.TryGetComponent(out itemComponent)) return false;  
        }

        //If not active returns false
        if(checkActive)
        {
            if(!itemComponent.GetIsActive()) return false;
        }

        //If corrupted returns false
        if(checkCorrupt)
        {
            if(itemComponent.GetIsCorrupt()) return false;
        }

        return true;
    }

    public int GetInventorySize()
    {
        return _maxItems;
    }
}
