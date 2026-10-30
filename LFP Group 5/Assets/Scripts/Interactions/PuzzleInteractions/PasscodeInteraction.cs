using UnityEngine;
using System;
using TMPro;

public class PasscodeInteraction : Interaction
{
    [Header("Required Item")]
    [SerializeField] private ItemSO _requiredItem;

    [Header("Door")]
    [SerializeField] private GameObject _lockedDoor, _unlockedDoor;

    [Header("Keypad")]
    [SerializeField] private GameObject _keypadCanvas;
    [SerializeField] private TMP_Text _passcodeDisplay;
    [SerializeField] private string _correctPasscode;

    [Header("Safe-only Settings")]
    [SerializeField] private GameObject _unlockedItem;

    private string _enteredPasscode = "";
    private Item _selectedItem;

    //public event Action OnDoorOpened;

    private void Awake()
    {
        if (_keypadCanvas != null)
            _keypadCanvas.SetActive(false);

        ClearPasscode();
    }

    public override void Interact(GameObject interactingObject, Item selectedItem)
    {
        if (!_canInteract) return;

        Player player = interactingObject.GetComponent<Player>();

        if (player == null)
        {
            Debug.LogWarning("Door interaction could not find Player.");
            InvokeFailedInteraction();
            return;
        }

        if (player.GetCurrentMode() != GameMode.NormalMode)
        {
            Debug.Log("Door cannot be opened in Safe Mode");
            return;
        }

        if (selectedItem == null)
        {
            Debug.Log("No item selected.");
            InvokeFailedInteraction();
            return;
        }

        if (selectedItem.GetItemSO() != _requiredItem)
        {
            Debug.Log("This item cannot open the door.");
            InvokeFailedInteraction();
            return;
        }

        _selectedItem = selectedItem;

        if (_keypadCanvas != null)
            _keypadCanvas.SetActive(true);

        ClearPasscode();

        base.Interact(interactingObject, selectedItem);
    }

    // This adds a digit to the keypad display.
    public void EnterDigit(string digit)
    {
        if (!_canInteract) return;
        if (_keypadCanvas == null || !_keypadCanvas.activeInHierarchy) return;
        if (_enteredPasscode.Length >= _correctPasscode.Length) return;

        _enteredPasscode += digit;

        if (_passcodeDisplay != null)
            _passcodeDisplay.text = _enteredPasscode;
    }

    // Checks the code when the Enter button is pressed.
    public void EnterPasscode()
    {
        if (!_canInteract) return;
        if (_keypadCanvas == null || !_keypadCanvas.activeInHierarchy) return;

        if (_enteredPasscode == _correctPasscode)
        {
            Debug.Log("CORRECT PASSCODE - ACCESS GRANTED");
            OpenDoor();
        }
        else
        {
            Debug.Log("INCORRECT PASSCODE - TRY AGAIN");
            InvokeFailedInteraction();
            ClearPasscode();
        }
    }

    // Clears the current input.
    public void ClearPasscode()
    {
        _enteredPasscode = "";

        if (_passcodeDisplay != null)
            _passcodeDisplay.text = "";
    }

    // Closes the keypad without opening the door.
    public void ExitKeypad()
    {
        if (_keypadCanvas != null)
            _keypadCanvas.SetActive(false);

        ClearPasscode();
        _selectedItem = null;
    }

    private void OpenDoor()
    {
        Debug.Log("DOOR OPENED");

        if (_selectedItem != null)
        {
            Player player = FindAnyObjectByType<Player>();

            if (player != null && player.GetInventory() != null)
            {
                player.GetInventory().RemoveItemFromInventory(_selectedItem.gameObject);
            }
        }

        if (_keypadCanvas != null)
            _keypadCanvas.SetActive(false);

        _lockedDoor.SetActive(false);
        _unlockedDoor.SetActive(true);

        if (_unlockedItem != null)
        {
            _unlockedItem.SetActive(true);
        }

        SetCanInteract(false);

        //OnDoorOpened?.Invoke();
    }
}
