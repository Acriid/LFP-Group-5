using UnityEngine;

public class DoorInteraction : Interaction
{
    [SerializeField] private ItemSO _requiredItem;
    [SerializeField] private GameObject _lockedDoor, _unlockedDoor;

    public override void Interact(GameObject interactingObject, Item selectedItem)
    {
        if (!_canInteract) return;

        if (selectedItem == null)
        {
            Debug.Log("No item selected.");
            return;
        }

        if (selectedItem.GetItemSO() != _requiredItem)
        {
            Debug.Log("This item cannot open the door.");
            return;
        }

        Debug.Log("Correct item used! Door opening.");

        Player player = interactingObject.GetComponent<Player>();

        if (player == null)
        {
            Debug.LogWarning("Door interaction could not find Player.");
            return;
        }

        Inventory inventory = player.GetInventory();

        if (inventory == null)
        {
            Debug.LogWarning("Player has no Inventory.");
            return;
        }

        inventory.RemoveItemFromInventory(selectedItem.gameObject);

        OpenDoor();
    }

    private void OpenDoor()
    {
        Debug.Log("DOOR OPENED");
        _lockedDoor.SetActive(false);
        _unlockedDoor.SetActive(true);

        SetCanInteract(false);
    }
}