using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class TimerFailedTrigger : MonoBehaviour
{
    [SerializeField] private ScriptableObjectDialogu _dialogue;
    private int _resetCount = 0;
    private bool _dialogueTriggered = false;

    public void PlayerHasReset()
    {
        if (_dialogueTriggered) return;

        _resetCount++;

        Debug.Log("PLAYER RESET COUNT: " + _resetCount);

        if (_resetCount == 2)
        {
            TriggerResetDialogue();
        }
    }

    private void TriggerResetDialogue()
    {
        if (_dialogue == null)
        {
            Debug.LogWarning("Reset dialogue has not been assigned.");
            return;
        }

        if (SOdialogueManager.Instance == null)
        {
            Debug.LogWarning("SOdialogueManager instance not found.");
            return;
        }

        _dialogueTriggered = true;

        SOdialogueManager.Instance.StartDialogue(_dialogue);

        Debug.Log("SECOND RESET - DIALOGUE TRIGGERED");
    }

}
