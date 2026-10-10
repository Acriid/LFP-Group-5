using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUiManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory = null;
    [SerializeField] private InputReader _inputReader = null;
    [SerializeField] private List<InventorySlot> _inventorySlots = new();
    [SerializeField] private GameObject _uiItemPrefab = null;
    [SerializeField] private Transform _dragParent = null;
    private GenericPool<UiItem> _uiItemPool;
    private UiItem _currentSelectedItem = null;

    private int _lockedIndex;

    void Awake()
    {
        InitializeInventory();
        InitializeItemPool();

        SubscribeInputs();
    }

    void OnDisable()
    {
        UnsubscribeEvents();
        UnSubScribeInputs();

        foreach(InventorySlot inventorySlot in _inventorySlots)
        {
            inventorySlot.OnHeldObjectChange -= ChangeItemSlots;
        }
    }
    void OnDestroy()
    {
        if(_currentSelectedItem == null) return;
        _currentSelectedItem.DeSelectItem(); 
    }


    private void SubscribeInputs()
    {
        _inputReader.OnSelect1 += SetSelectedItem;
        _inputReader.OnSelect2 += SetSelectedItem;
        _inputReader.OnSelect3 += SetSelectedItem;
        _inputReader.OnSelect4 += SetSelectedItem;

        _inputReader.EnableSelectItem1Action();
        _inputReader.EnableSelectItem2Action();
        _inputReader.EnableSelectItem3Action();
        _inputReader.EnableSelectItem4Action();
    }
    private void UnSubScribeInputs()
    {
        _inputReader.OnSelect1 -= SetSelectedItem;
        _inputReader.OnSelect2 -= SetSelectedItem;
        _inputReader.OnSelect3 -= SetSelectedItem;
        _inputReader.OnSelect4 -= SetSelectedItem; 

        _inputReader.DisableSelectItem1Action();
        _inputReader.DisableSelectItem2Action();
        _inputReader.DisableSelectItem3Action();
        _inputReader.DisableSelectItem4Action();
    }

    private void InitializeInventory()
    {
        if(_inventory == null) return;
        if(_inventorySlots.Count != _inventory.GetInventorySize())
        {
            Debug.LogWarning("Inventory size not equal to slot count");
            _inventory.SetAllowedSize(_inventorySlots.Count);
        }


        foreach(InventorySlot inventorySlot in _inventorySlots)
        {
            inventorySlot.OnHeldObjectChange += ChangeItemSlots;
        }


        _inventory.OnItemPickup += AddItem;
        _inventory.OnItemRemove += RemoveItem;  
        _inventory.OnMaxSizeChange += LockSlots;     
    }
    private void InitializeItemPool()
    {
        if(_inventory == null) return;
        _uiItemPool = PoolManager.Instance.GetUIPool<UiItem>(_uiItemPrefab,_inventorySlots.Count);
        if(_uiItemPool == null)
        {
            Debug.LogError("Failed to load uiItem pool.");
        }

        foreach(UiItem uiItem in _uiItemPool)
        {
            uiItem.OnItemClicked += SetSelectedItem;
            if(uiItem.TryGetComponent(out ItemDragHandler dragHandlerComponent))
            {
                dragHandlerComponent.SetDragParent(_dragParent);
            }
        }
    }

    private void SetSelectedItem(int itemSlot)
    {
        if(itemSlot > _inventorySlots.Count) return;

        GameObject itemToSelect = _inventorySlots[itemSlot - 1].GetHeldObject();
        if(itemToSelect == null) return;

        if(!itemToSelect.TryGetComponent(out UiItem uiItemComponent)) return;

        SetSelectedItem(uiItemComponent);
    }
    private void SetSelectedItem(UiItem newItem)
    {
        foreach(UiItem uiItem in _uiItemPool)
        {
            uiItem.DeSelectItem();
        }
        
        if(_currentSelectedItem == newItem && _currentSelectedItem != null)
        {
            _currentSelectedItem.DeSelectItem();
            _currentSelectedItem = null;
            return;
        }

        _currentSelectedItem = newItem;
        if(_currentSelectedItem == null) return;
        _currentSelectedItem.SelectItem();
    }
    public UiItem GetCurrentSelectedItem()
    {
        return _currentSelectedItem;
    }


    private void UnsubscribeEvents()
    {
        foreach(UiItem uiItem in _uiItemPool)
        {
            uiItem.OnItemClicked -= SetSelectedItem;
        }

        if(_inventory == null) return;
        _inventory.OnItemPickup -= AddItem;
        _inventory.OnItemRemove -= RemoveItem;   
        _inventory.OnMaxSizeChange -= LockSlots;     
    }
    private void ChangeItemSlots(InventorySlot inventorySlot)
    {
        _inventory.ChangeItemSlot(inventorySlot.GetHeldObject(),_inventorySlots.IndexOf(inventorySlot));
    }
    private void LockSlots(int allowedSlots)
    {
        int slotCount = _inventorySlots.Count;

        if(allowedSlots < slotCount)
        {
            for(int i = slotCount -1; i > allowedSlots - 1 ; i--)
            {
                _inventorySlots[i].SetIsActive(false);
                
            }
            _lockedIndex = allowedSlots;
        }
        else if(allowedSlots >= slotCount)
        {
            for(int i = _lockedIndex; i < slotCount ; i++)
            {
                _inventorySlots[i].SetIsActive(true);
            }        
        }     
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
                    uiItem.DeSelectItem();
                    uiItem.SetItem(null);
                    uiItem.GetItemDragHandler().SetItemSO(null);
                    inventorySlot.SetHeldObject(null);
                    _uiItemPool.Return(uiItem);

                    SetSelectedItem(null);
                }
            }
        }       
    }


    public void SetNormalModeSprite()
    {
        foreach(UiItem uiItem in _uiItemPool)
        {
            uiItem.SetNormalModeUISprite();
        }
    }
    public void SetSafeModeSprite()
    {
        foreach(UiItem uiItem in _uiItemPool)
        {
            uiItem.SetSafeModeUISprite();
        }        
    }
}
