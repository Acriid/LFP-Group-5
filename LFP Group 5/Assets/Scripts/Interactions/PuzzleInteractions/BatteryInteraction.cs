using Unity.VisualScripting;
using UnityEngine;

public class BatteryInteraction : Interaction
{
    [SerializeField] private ItemSO _requiredItem;
    [SerializeField] private GameObject _shadow, _revealKey, _otherBattery;
    [SerializeField] private TimerControlPanel _panel;

    [SerializeField] private GameObject _disableObject = null;

    public override void Interact(GameObject interactingObject, Item selectedItem)
    {
        if (!_canInteract) return;

        if (selectedItem == null)
        {
            Debug.Log("No item selected.");
            InvokeFailedInteraction();
            return;
        }

        if (selectedItem.GetItemSO() != _requiredItem)
        {
            Debug.Log("This item cannot used.");
            InvokeFailedInteraction();
            return;
        }

        Debug.Log("Correct item used! Door opening.");

        Player player = interactingObject.GetComponent<Player>();

        if (player == null)
        {
            Debug.LogWarning("Interaction could not find Player.");
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

        LightUp();
        DisableObjects();

        if(_panel != null)
        {
            _panel.StopTimer();
        }


        PlayRandomInteractionSound();
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
    public void ResetLight()
    {
        _shadow.SetActive(true);
        _revealKey.SetActive(false);

        if (_otherBattery != null)
        {
            _otherBattery.SetActive(false);
        }

        SetCanInteract(true);       
    }
    private void DisableObjects()
    {
        if(_disableObject == null) return;
        _disableObject.SetActive(false);
    }
}
