using System.Collections.Generic;
using UnityEngine;

public class ItemPositionReset : MonoBehaviour
{
    [SerializeField] private List<Item> _itemsToTrack = new();

    private Dictionary<Item, Vector3> _originalPositions = new();

    private void Awake()
    {
        foreach (Item item in _itemsToTrack)
        {
            if (item == null) continue;

            _originalPositions[item] = item.transform.position;
        }
    }

    public void ResetItems(Inventory inventory)
    {
        foreach (Item item in _itemsToTrack)
        {
            if (item == null) continue;

            // Remove item from inventory if player is carrying it
            if (inventory != null)
            {
                inventory.ForceRemoveItem(item.gameObject);
            }

            // Restore original position
            item.transform.position = _originalPositions[item];

            // Make item visible again
            item.gameObject.SetActive(true);
        }

        Debug.Log("PUZZLE ITEMS RESET TO ORIGINAL POSITIONS");
    }
}
