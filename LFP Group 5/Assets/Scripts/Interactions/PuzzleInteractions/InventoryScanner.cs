using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScanner : Interaction
{
    [Header("Scanner")]
    [SerializeField] private GameObject _scannerPanel;
    [SerializeField] private List<ItemSO> _requiredItems = new();

    [Header("Door Movement")]
    [SerializeField] private bool _isHorizontal;
    [SerializeField] private float _gridCellSize = 1f;
    [SerializeField] private float _slideSpeed = 3f;

    public override void Interact(GameObject interactingObject)
    {
        if (!_canInteract)
            return;

        // Get the player interacting with the scanner
        Player player = interactingObject.GetComponent<Player>();

        if (player == null)
        {
            Debug.LogWarning("Scanner could not find Player.");
            return;
        }

        // Get the player's inventory
        Inventory inventory = player.GetInventory();

        if (inventory == null)
        {
            Debug.LogWarning("Scanner could not find Player Inventory.");
            return;
        }

        ScanInventory(inventory);
    }

    private void ScanInventory(Inventory inventory)
    {
        Debug.Log("SCANNING PLAYER INVENTORY...");

        StartCoroutine(ScannerOpen(inventory));
    }

    private IEnumerator ScannerOpen(Inventory inventory)
    {
        List<Item> inventoryItems = inventory.GetItemList();

        if (_scannerPanel != null)
        {
            _scannerPanel.SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        if (_scannerPanel != null)
        {
            _scannerPanel.SetActive(false);
        }
            

        bool scanSuccessful = true;

        foreach (ItemSO requiredItem in _requiredItems)
        {
            bool itemFound = false;

            foreach (Item inventoryItem in inventoryItems)
            {
                if (inventoryItem == null)
                    continue;

                if (inventoryItem.GetItemSO() == requiredItem)
                {
                    itemFound = true;
                    break;
                }
            }

            if (!itemFound)
            {
                scanSuccessful = false;
            }
        }

        if (scanSuccessful)
        {
            Debug.Log("SCAN SUCCESSFUL - ACCESS GRANTED");
            OpenDoor();
        }
        else
        {
            string requiredItemNames = "";

            foreach (ItemSO requiredItem in _requiredItems)
            {
                if (requiredItemNames != "")
                {
                    requiredItemNames += ", ";
                }

                requiredItemNames += requiredItem.ItemDescription;
            }

            Debug.Log("Clearance for: " + requiredItemNames + " needed to enter.");
        }
    }

    private void OpenDoor()
    {
        // This stops the player from scanning again once the door opens
        SetCanInteract(false);

        StartCoroutine(SlideDoorOpen());
    }

    private IEnumerator SlideDoorOpen()
    {
        Vector3 startPosition = transform.position;

        Vector3 direction;

        if (_isHorizontal)
        {
            // Horizontal door slides 3 units to the right
            direction = Vector3.right;
        }
        else
        {
            // Vertical door slides 3 units downward
            direction = Vector3.down;
        }

        Vector3 targetPosition =
            startPosition + direction * (_gridCellSize * 3f);

        // Smoothly slide the door
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                _slideSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = targetPosition;

        Debug.Log("SCANNER DOOR OPENED");
    }
}