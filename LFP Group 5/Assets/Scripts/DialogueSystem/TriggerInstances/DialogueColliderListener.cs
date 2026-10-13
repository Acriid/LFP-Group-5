using UnityEngine;

public class DialogueColliderListener : MonoBehaviour
{
    private TriggerColliderDialogue _dialogueTrigger = null;
    private bool _normalModeDialog = true;
    private Player _player = null;
    void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }
    public void SetDialogueTrigger(TriggerColliderDialogue dialogueTrigger, bool normalModeDialog = true)
    {
        _dialogueTrigger = dialogueTrigger;
        _normalModeDialog = normalModeDialog;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_dialogueTrigger == null)
            return;

        if (!other.CompareTag("Player"))
            return;

        if(_normalModeDialog)
        {
            if (_player.GetCurrentMode() != GameMode.NormalMode) return;
        }
        else
        {
            if (_player.GetCurrentMode() == GameMode.NormalMode) return;
        }

        _dialogueTrigger.TriggerDialogue();
    }
}
