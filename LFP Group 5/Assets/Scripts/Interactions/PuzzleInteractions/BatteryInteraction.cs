using UnityEngine;

public class BatteryInteraction : Interaction
{
    [SerializeField] private ItemSO _requiredItem;
    [SerializeField] private GameObject _shadow, _revealKey, _otherBattery;
    [SerializeField] private TimerControlPanel _panel;

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
            Debug.Log("This item cannot used.");
            return;
        }

        Debug.Log("Correct item used! Door opening.");

        Player player = interactingObject.GetComponent<Player>();

        if (player == null)
        {
            Debug.LogWarning("Interaction could not find Player.");
            return;
        }

        Inventory inventory = player.GetInventory();

        if (inventory == null)
        {
            Debug.LogWarning("Player has no Inventory.");
            return;
        }

        inventory.RemoveItemFromInventory(selectedItem.gameObject);

        LightUp();

        if(_panel != null)
        {
            _panel.StopTimer();
        }

        base.Interact(interactingObject,selectedItem);
    }

    private void LightUp()
    {
        _shadow.SetActive(false);
        _revealKey.SetActive(true);

        if (_otherBattery != null)
        {
            _otherBattery.SetActive(true);
        }

        SetCanInteract(false);
    }
}
