using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

// ok this script is necessary as it is responsible for triggering dialogue
// when the passwords have been collected.
// this is on LVL 1 P1

public class InteractionDialogueTrigger : MonoBehaviour
{
    // list items needed
    [SerializeField] private List<ItemSO> _requiredItems = new List<ItemSO>();

    [SerializeField] private ScriptableObjectDialogu _dialogueTrigger;

    private List<ItemSO> _collectedItems = new List<ItemSO>();

    private Inventory inventory;

    private bool _dialogueTriggered;

    void Start()
    {
        Player player = FindFirstObjectByType<Player>();

        inventory = player.GetInventory();

        if (inventory == null)
        {
            Debug.Log("InteractionDialogueTrigger: Nothing in inventory");
        }

        inventory.OnItemPickup += OnItemPickedUp;
    }

    private void OnItemPickedUp(Item item)
    {
        if (item == null)
            return;

        ItemSO collectedItem = item.GetItemSO();

        if (!_requiredItems.Contains(collectedItem))
        {
            return;
        }

        // Don't add the same item twice
        if (_collectedItems.Contains(collectedItem))
        {
            return;
        }

        _collectedItems.Add(collectedItem);

        CheckRequiredItems();
    }

    private void CheckRequiredItems()
    {
        if (_dialogueTriggered)
        {
            return;
        }

        foreach (ItemSO requiredItem in _requiredItems)
        {
            if (!_collectedItems.Contains(requiredItem))
            {
                return;
            }
        }

        _dialogueTriggered = true;

        if (SOdialogueManager.Instance != null)
        {
            SOdialogueManager.Instance.StartDialogue(_dialogueTrigger);
        }
        else
        {
            Debug.LogWarning("InteractionDialogueTrigger: No SOdialogueManager found.");
        }
    }

    private void OnDestroy()
    {
        if (inventory != null)
        {
            inventory.OnItemPickup -= OnItemPickedUp;
        }
    }
}
