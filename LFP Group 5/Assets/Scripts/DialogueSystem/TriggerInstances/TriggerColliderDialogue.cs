using UnityEngine;

public class TriggerColliderDialogue : MonoBehaviour
{
    [SerializeField] private Collider2D _triggerCollider;
    [SerializeField] private ScriptableObjectDialogu _dialogue;

    private bool _hasTriggered = false;

    private void Start()
    {
        if (_triggerCollider == null)
        {
            Debug.LogWarning("DialogueTriggerSO: No trigger collider assigned.");
            return;
        }

        DialogueColliderListener listener = _triggerCollider.GetComponent<DialogueColliderListener>();

        if (listener == null)
        {
            listener = _triggerCollider.gameObject.AddComponent<DialogueColliderListener>();
        }

        listener.SetDialogueTrigger(this);
    }

    public void TriggerDialogue()
    {
        if (_hasTriggered)
            return;

        if (_dialogue == null)
        {
            Debug.LogWarning("DialogueTriggerSO: No dialogue SO assigned.");
            return;
        }

        if (SOdialogueManager.Instance == null)
        {
            Debug.LogWarning("DialogueTriggerSO: No SOdialogueManager found.");
            return;
        }

        _hasTriggered = true;

        SOdialogueManager.Instance.StartDialogue(_dialogue);
    }
}