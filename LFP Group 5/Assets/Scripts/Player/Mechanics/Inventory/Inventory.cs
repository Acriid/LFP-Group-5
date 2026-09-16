using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> _itemList;
    //When max items gets smaller items drop from back of the list.
    [SerializeField] private int _maxItems = 4;
    private int _currentItems = 0;
    private int _lockedIndex = 0;
    public event Action<Item> OnItemPickup;
    public event Action<Item> OnItemRemove;
    public event Action<int> OnMaxSizeChange;
    //Should inventory keep track of the current item?

    void OnEnable()
    {
        _itemList = new(_maxItems);
    }

    public void SetMaxSize(int newValue)
    {
        _maxItems = newValue;

        if(_maxItems < _currentItems)
        {
            for(int i = _currentItems -1; i > _maxItems - 1 ; i--)
            {
                _itemList[i].SetIsActive(false);
                
            }
            _lockedIndex = _maxItems;
        }
        else if(_maxItems >= _currentItems)
        {
            for(int i = _lockedIndex; i < _currentItems ; i++)
            {
                _itemList[i].SetIsActive(true);
            }        
        }
        OnMaxSizeChange?.Invoke(_maxItems);
    }

    public void AddToInventoryItem(GameObject itemToAdd)
    {
        if(itemToAdd == null) return;
        if(_currentItems >= _maxItems) return;
        if(!CheckIfItem(itemToAdd,out Item itemComponent,false,true))return;
        if(_itemList.Contains(itemComponent)) return;

        itemToAdd.SetActive(false);
        _itemList.Add(itemComponent);
        _currentItems++;
        OnItemPickup?.Invoke(itemComponent);
    }
    public GameObject RemoveItemFromInventory()
    {
        GameObject itemRemoved = null;
        foreach(Item item in _itemList)
        {
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
        if(!_itemList.Contains(itemComponent)) return null;

        _itemList.Remove(itemComponent);
        _currentItems--;
        OnItemRemove?.Invoke(itemComponent);
        return itemComponent.gameObject;
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
            Debug.Log("Corrupt");
        }

        return true;
    }

    public int GetInventorySize()
    {
        return _maxItems;
    }
}
