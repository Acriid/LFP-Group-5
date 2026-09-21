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
    [Header("Password Items")]
    [SerializeField] private ItemSO _passwordIncorrect;
    [SerializeField] private ItemSO _passwordCorrect;

    [SerializeField] private ScriptableObjectDialogu _dialogueTrigger;

    private bool _incorrectCollected;
    private bool _correctCollected;

    private Inventory inventory;

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

        if (collectedItem == _passwordIncorrect)
        {
            _incorrectCollected = true;
        }

        if (collectedItem == _passwordCorrect)
        {
            _correctCollected = true;
        }

        CheckPasswordItems();
    }

    private void CheckPasswordItems()
    {
        if (!_incorrectCollected || !_correctCollected)
            return;

        if (SOdialogueManager.Instance != null)
        {
            SOdialogueManager.Instance.StartDialogue(_dialogueTrigger);
        }
        else
        {
            Debug.LogWarning ("InteractionDialogueTrigger: No SOdialogueManager found.");
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
