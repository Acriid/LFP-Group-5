using System.Collections.Generic;

using UnityEngine;

public class InventoryUiManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory = null;
    [SerializeField] private List<InventorySlot> _inventorySlots = new();
    [SerializeField] private GameObject _uiItemPrefab = null;

    void Awake()
    {
        if(_inventorySlots.Count != _inventory.GetInventorySize())
        {
            Debug.LogWarning("Inventory size not equal to slot count");
        }

        _inventory.OnItemPickup += AddItem;
        _inventory.OnItemRemove += RemoveItem;
    }
    void OnDisable()
    {
        _inventory.OnItemPickup -= AddItem;
        _inventory.OnItemRemove -= RemoveItem;
    }
    private void AddItem(Item itemToInitialize)
    {
        if(itemToInitialize == null) return;
        foreach(InventorySlot inventorySlot in _inventorySlots)
        {
            GameObject heldObject = inventorySlot.GetHeldObject();
            if (heldObject == null)
            {
                heldObject = Instantiate(_uiItemPrefab);
                inventorySlot.SetHeldObject(_uiItemPrefab,inventorySlot.transform);

            }

            if(!heldObject.TryGetComponent(out UiItem uiItem)) return;
            if(uiItem != null && !uiItem.GetHasItem())
            {
                uiItem.SetItem(itemToInitialize);
            }

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
                if(!heldObject.TryGetComponent<UiItem>(out var uiItem)) return;
                if(uiItem.GetItem() == itemToRemove)
                uiItem.SetItem(null);
            }
        }       
    }
}
