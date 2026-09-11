using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUiManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory = null;
    [SerializeField] private List<InventorySlot> _inventorySlots = new();
    [SerializeField] private GameObject _uiItemPrefab = null;

    private GenericPool<UiItem> _uiItemPool;
    private UiItem _currentSelectedItem = null;
    void Awake()
    {
        InitializeInventory();
        InitializeItemPool();
    }
    private void InitializeInventory()
    {
        if(_inventory == null) return;
        if(_inventorySlots.Count != _inventory.GetInventorySize())
        {
            Debug.LogWarning("Inventory size not equal to slot count");
            _inventory.ChangeMaxItems(_inventorySlots.Count);
        }

        _inventory.OnItemPickup += AddItem;
        _inventory.OnItemRemove += RemoveItem;       
    }
    private void InitializeItemPool()
    {
        if(_inventory == null) return;
        _uiItemPool = PoolManager.Instance.GetPool<UiItem>(_uiItemPrefab,_inventorySlots.Count);
        if(_uiItemPool == null)
        {
            Debug.LogError("Failed to load bullet pool.");
        }

        foreach(UiItem uiItem in _uiItemPool)
        {
            uiItem.OnItemClicked += ChangeSelectedItem;
        }
    }

    private void ChangeSelectedItem(UiItem newItem)
    {
        _currentSelectedItem = newItem;
    }

    void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void UnsubscribeEvents()
    {
        foreach(UiItem uiItem in _uiItemPool)
        {
            uiItem.OnItemClicked -= ChangeSelectedItem;
        }

        if(_inventory == null) return;
        _inventory.OnItemPickup -= AddItem;
        _inventory.OnItemRemove -= RemoveItem;      
    }
    private void AddItem(Item itemToInitialize)
    {
        if(itemToInitialize == null) return;
        
        foreach(InventorySlot inventorySlot in _inventorySlots)
        {
            GameObject heldObject = inventorySlot.GetHeldObject();
            if (heldObject != null) continue;

            UiItem pooledItem = _uiItemPool.Get();
            pooledItem.GetItemDragHandler().SetItemSO(itemToInitialize.GetItemSO());
            heldObject = pooledItem.gameObject;
            inventorySlot.SetHeldObject(heldObject, inventorySlot.transform);

            if(!heldObject.TryGetComponent(out UiItem uiItem)) return;
            if(uiItem != null && !uiItem.GetHasItem())
            {
                uiItem.SetItem(itemToInitialize);
            }

            return;
        }
    }

    private void RemoveItem(Item itemToRemove)
    {
        if(itemToRemove == null) return;
        foreach(InventorySlot inventorySlot in _inventorySlots)
        {
            GameObject heldObject = inventorySlot.GetHeldObject();
            if(heldObject != null)
            {
                if(!heldObject.TryGetComponent(out UiItem uiItem)) return;
                if(uiItem.GetItem() == itemToRemove)
                {
                    uiItem.SetItem(null);
                    uiItem.GetItemDragHandler().SetItemSO(null);
                    _uiItemPool.Return(uiItem);
                }
            }
        }       
    }
}
