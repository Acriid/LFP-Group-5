using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class InventoryScanner : Interaction
{
    [Header("Scanner")]
    [SerializeField] private Image _scannerPanel;
    [SerializeField] private TMP_Text _scannerText;
    [SerializeField] private List<ItemSO> _requiredItems = new();

    [Header("Door Movement")]
    [SerializeField] private bool _isHorizontal;
    [SerializeField] private float _gridCellSize = 1f;
    [SerializeField] private float _slideSpeed = 3f;
    [SerializeField] private float _scanTime = 1f;

    private Color _originalColor;
    void OnEnable()
    {
        if (_scannerPanel != null)
        _originalColor = _scannerPanel.color;
    }
    public override void Interact(GameObject interactingObject)
    {
        if (!_canInteract)
            return;

        // Get the player interacting with the scanner

        if (!interactingObject.TryGetComponent<Player>(out Player player))
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
            _scannerPanel.color = _originalColor;
            _scannerPanel.gameObject.SetActive(true);
        }
        

        // Shows scanning
        float elapsedTime = 0f;
        string startText = "Scanning";
        int dotCount = 0;
        float elapsedTime2 = 0f;
        while(elapsedTime < _scanTime)
        {
            
            if(elapsedTime2 > 0.5f)
            {
                elapsedTime2 = 0f;
                startText = "Scanning";
                for(int i = 0; i < dotCount; i++)
                {
                    startText += ".";
                }
                dotCount++;
                if(dotCount >= 4)
                {
                    dotCount = 0;
                }
            }

            if(_scannerText != null)
            _scannerText.text = startText;

            elapsedTime2 += Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        

        bool scanSuccessful = true;
        int missingItems = 0;

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
                missingItems++;
            }
        }

        Color failedColor = Color.red;
        failedColor.a = _originalColor.a;
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
            
            _scannerPanel.color = failedColor;
            if(_scannerText != null)
            _scannerText.text = $"Scan Failed Missing {missingItems} ID";

            yield return new WaitForSeconds(4f);
        }

        

        if (_scannerPanel != null)
        {
            _scannerPanel.gameObject.SetActive(false);
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