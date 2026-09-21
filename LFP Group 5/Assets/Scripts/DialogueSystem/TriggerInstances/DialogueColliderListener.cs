using UnityEngine;

public class DialogueColliderListener : MonoBehaviour
{
    private TriggerColliderDialogue _dialogueTrigger;

    public void SetDialogueTrigger(TriggerColliderDialogue dialogueTrigger)
    {
        _dialogueTrigger = dialogueTrigger;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_dialogueTrigger == null)
            return;

        if (!other.CompareTag("Player"))
            return;

        _dialogueTrigger.TriggerDialogue();
    }
}
