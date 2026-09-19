using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> _itemList;
    //When max items gets smaller items drop from back of the list.
    [SerializeField] private int _maxItems = 4;
    private int _allowedSize = 0;
    private int _currentItems = 0;
    private int _lockedIndex = 0;
    public event Action<Item> OnItemPickup;
    public event Action<Item> OnItemRemove;
    public event Action<int> OnMaxSizeChange;
    //Should inventory keep track of the current item?

    void OnEnable()
    {
        InitializeList();
        _allowedSize = _maxItems;
    }

    private void InitializeList()
    {
        _itemList = new(_maxItems);
        for(int i = 0; i < _maxItems; i++)
        {
            _itemList.Add(null);
        }
        _allowedSize = _maxItems;
    }
    public void SetAllowedSize(int newValue)
    {
        _allowedSize = newValue;

        if(_allowedSize < _currentItems)
        {
            for(int i = _itemList.Count - 1; i > _allowedSize - 1 ; i--)
            {
                if(_itemList[i] == null) continue;
                _itemList[i].SetIsActive(false);
                
            }
            _lockedIndex = _allowedSize;
        }
        else if(_allowedSize >= _currentItems)
        {
            for(int i = _lockedIndex; i < _itemList.Count ; i++)
            {
                if(_itemList[i] == null) continue;
                _itemList[i].SetIsActive(true);
            }        
        }
        OnMaxSizeChange?.Invoke(_allowedSize);
    }

    public void AddToInventoryItem(GameObject itemToAdd)
    {
        if(itemToAdd == null) return;
        if(_currentItems >= _maxItems) return;
        if(!CheckIfItem(itemToAdd,out Item itemComponent,false,true))return;
        if(_itemList.Contains(itemComponent)) return;

        int openSlot = -1;
        for(int i = 0; i < _allowedSize; i++)
        {
            if(_itemList[i] == null)
            {
                openSlot = i;
                break;
            }
        }
        if(openSlot < 0) return;

        itemToAdd.SetActive(false);
        _itemList[openSlot] = itemComponent;
        _currentItems++;
        OnItemPickup?.Invoke(itemComponent);
    }
    public GameObject RemoveItemFromInventory()
    {
        GameObject itemRemoved = null;
        foreach(Item item in _itemList)
        {
            if(item == null) continue;
            if(item.GetItemSO().IsSelected)
            {
                itemRemoved = item.gameObject;
                RemoveItemFromInventory(item.gameObject);
                break;
            }
        }
        return itemRemoved;
    }
    public GameObject RemoveItemFromInventory(GameObject itemToRemove)
    {
        if(itemToRemove == null) return null;
        if(!CheckIfItem(itemToRemove,out Item itemComponent,true))return null;

        int index = _itemList.IndexOf(itemComponent);
        if(index < 0) return null;

        _itemList[index] = null;
        _currentItems--;
        OnItemRemove?.Invoke(itemComponent);
        return itemComponent.gameObject;
    }
    //TODO - Change Item Slots in UIInventory.
    public void ChangeItemSlot(GameObject itemToChange, int newSlot)
    {
        if (newSlot < 0 || newSlot >= _maxItems) return;
        if(!CheckIfItem(itemToChange,out Item itemComponent,true)) return;
        
        int originalSlot = _itemList.IndexOf(itemComponent);
        if(originalSlot == newSlot) return;
        if (originalSlot < 0) return;

        Debug.Log("Change");
        (_itemList[originalSlot], _itemList[newSlot]) = (_itemList[newSlot], _itemList[originalSlot]);
    }

    private bool CheckIfItem(GameObject objectToCheck, out Item itemComponent, bool checkActive = false, bool checkCorrupt = false)
    {
        if(objectToCheck == null)
        {
            itemComponent = null;
            return false;
        }
        
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
            Debug.Log(!itemComponent.GetIsActive());
            if(!itemComponent.GetIsActive()) return false;
        }

        //If corrupted returns false
        if(checkCorrupt)
        {
            if(itemComponent.GetIsCorrupt()) return false;
            Debug.Log("Corrupt");
        }

        return true;
    }

    public int GetInventorySize()
    {
        return _maxItems;
    }

    //method to get a selected item
    public Item GetSelectedItem()
    {
        foreach (Item item in _itemList)
        {
            if (item == null) continue;

            if (item.GetItemSO().IsSelected)
            {
                return item;
            }
        }

        return null;
    }
}
