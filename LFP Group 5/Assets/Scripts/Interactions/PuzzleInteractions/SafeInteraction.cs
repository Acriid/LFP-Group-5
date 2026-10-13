using UnityEngine;
using System;

public class SafeInteraction : Interaction
{
    [SerializeField] private ItemSO _requiredItem;
    [SerializeField] private GameObject _lockedSafe, _unlockedSafe, _unlockedItem;

    // hello Gemma here. Need to add this to trigger dialogue.
    public event Action OnDoorOpened;

    public override void Interact(GameObject interactingObject, Item selectedItem)
    {
        if (!_canInteract) return;

        Player player = interactingObject.GetComponent<Player>();

        if (player.GetCurrentMode() != GameMode.NormalMode)
        {
            Debug.Log("Safe cannot be opened in Safe Mode");
            return;
        }

        if (selectedItem == null)
        {
            Debug.Log("No item selected.");
            return;
        }

        if (selectedItem.GetItemSO() != _requiredItem)
        {
            Debug.Log("This item cannot open the safe.");
            return;
        }

        Debug.Log("Correct item used! Safe opening.");

        if (player == null)
        {
            Debug.LogWarning("Safe interaction could not find Player.");
            return;
        }

        Inventory inventory = player.GetInventory();

        if (inventory == null)
        {
            Debug.LogWarning("Player has no Inventory.");
            return;
        }

        inventory.RemoveItemFromInventory(selectedItem.gameObject);

        OpenSafe();
    }

    private void OpenSafe()
    {
        Debug.Log("DOOR OPENED");
        _lockedSafe.SetActive(false);
        _unlockedSafe.SetActive(true);
        _unlockedItem.SetActive(true);

        SetCanInteract(false);

        // hello Gemma here. Need to add this to trigger dialogue.
        OnDoorOpened?.Invoke();
    }
}
