using System.Collections.Generic;
using UnityEngine;

public class InventoryUiManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory = null;
    [SerializeField] private List<InventorySlot> _inventorySlots = new();

    void Awake()
    {
        if(_inventorySlots.Count != _inventory.GetInventorySize())
        {
            Debug.LogWarning("Inventory size not equal to slot count");
        }
    }
}
