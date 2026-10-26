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
    private Vector3 _closedPosition;

    [Header("Player")]
    [SerializeField] private InputReader _inputReader;
    private Color _originalColor;
    private Coroutine _scanningRoutine = null;
    private Coroutine _slideRoutine = null;
    private Vector2 _targetPosition = Vector2.zero;
    void OnEnable()
    {
        _closedPosition = transform.position;

        if (_scannerPanel != null)
            _originalColor = _scannerPanel.color;
    }
    void OnDisable()
    {
        if(_scanningRoutine != null)
        {
            StopCoroutine(_scanningRoutine);
            _scanningRoutine = null;
            ResetScanPanel();
            _inputReader.EnablePlayerActions();
        }
        if(_slideRoutine != null)
        {
            StopCoroutine(_slideRoutine);
            _slideRoutine = null;
            
            //Finish slide
            transform.position = _targetPosition;
        }
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

        _scanningRoutine ??= StartCoroutine(ScannerOpen(inventory));
    }

    private IEnumerator ScannerOpen(Inventory inventory)
    {
        List<Item> inventoryItems = inventory.GetItemList();

        if (_scannerPanel != null)
        {
            _scannerPanel.color = _originalColor;
            _scannerPanel.gameObject.SetActive(true);
        }
        

        _inputReader.DisablePlayerActions();


        //0 is scanning
        PlayInteractionSound(_interactionAudioSet.AudioList[0]);

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

            PlayRandomFailedInteractionSound();
            yield return new WaitForSeconds(3f);

            ResetScanPanel();
            _inputReader.EnablePlayerActions();
        }

        _scanningRoutine = null;

    }

    private void OpenDoor()
    {
        // This stops the player from scanning again once the door opens
        SetCanInteract(false);

        ResetScanPanel();
        _inputReader.EnablePlayerActions();

        if(_slideRoutine == null)
        _slideRoutine = StartCoroutine(SlideDoorOpen());
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

        _targetPosition =
            startPosition + direction * (_gridCellSize * 3f);

        //1 is success
        PlayInteractionSound(_interactionAudioSet.AudioList[1]);

        // Smoothly slide the door
        while (Vector3.Distance(transform.position, _targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _targetPosition,
                _slideSpeed * Time.deltaTime
            );

            yield return null;
        }


        transform.position = _targetPosition;

        Debug.Log("SCANNER DOOR OPENED");
        _slideRoutine = null;
    }


    private void ResetScanPanel()
    {
        if(_scannerPanel == null) return;

        _scannerText.text = "";
        _scannerPanel.color = _originalColor;
        _scannerPanel.gameObject.SetActive(false);
    }



    //This'll reset the door when timer fails
    public void ResetScannerDoor()
    {
        // Stop any existing door movement
        if (_slideRoutine != null)
        {
            StopCoroutine(_slideRoutine);
            _slideRoutine = null;
        }

        // Allow the scanner to be used again
        SetCanInteract(true);

        ResetScanPanel();

        if (_inputReader != null)
            _inputReader.EnablePlayerActions();

        // Start moving the door back to its original position
        _slideRoutine = StartCoroutine(SlideDoorClosed());
    }

    private IEnumerator SlideDoorClosed()
    {
        while (Vector3.Distance(transform.position, _closedPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _closedPosition,
                _slideSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = _closedPosition;

        Debug.Log("SCANNER DOOR CLOSED");

        _slideRoutine = null;
    }
}