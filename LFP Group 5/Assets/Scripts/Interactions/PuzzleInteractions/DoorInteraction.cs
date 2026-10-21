using UnityEngine;
using System;

public class DoorInteraction : Interaction
{
    [SerializeField] private ItemSO _requiredItem;
    [SerializeField] private GameObject _lockedDoor, _unlockedDoor;

    // hello Gemma here. Need to add this to trigger dialogue.
    public event Action OnDoorOpened;

    public override void Interact(GameObject interactingObject, Item selectedItem)
    {
        if (!_canInteract) return;

        Player player = interactingObject.GetComponent<Player>();

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

        Debug.Log("Correct item used! Door opening.");

        if (player == null)
        {
            Debug.LogWarning("Door interaction could not find Player.");
            InvokeFailedInteraction();
            return;
        }

        Inventory inventory = player.GetInventory();

        if (inventory == null)
        {
            Debug.LogWarning("Player has no Inventory.");
            InvokeFailedInteraction();
            return;
        }

        inventory.RemoveItemFromInventory(selectedItem.gameObject);

        OpenDoor();

        base.Interact(interactingObject,selectedItem);
    }

    private void OpenDoor()
    {
        Debug.Log("DOOR OPENED");
        _lockedDoor.SetActive(false);
        _unlockedDoor.SetActive(true);

        SetCanInteract(false);

        // hello Gemma here. Need to add this to trigger dialogue.
        OnDoorOpened?.Invoke();
    }
}