using UnityEngine;

public class DoorDialogueTrigger : MonoBehaviour
{
    [SerializeField] private DoorInteraction _door;
    [SerializeField] private ScriptableObjectDialogu _dialogue;

    private bool _hasTriggered;

    private void Start()
    {
        if (_door == null)
        {
            Debug.LogWarning("DoorDialogueTrigger: No DoorInteraction assigned.");
            return;
        }

        _door.OnDoorOpened += OnDoorOpened;
    }

    private void OnDoorOpened()
    {
        if (_hasTriggered)
        {
            return;
        }
        
        if (_dialogue == null)
        {
            Debug.LogWarning("DoorDialogueTrigger: No dialogue SO assigned.");
            return;
        }

        if (SOdialogueManager.Instance == null)
        {
            Debug.LogWarning("DoorDialogueTrigger: No SOdialogueManager found.");
            return;
        }

        _hasTriggered = true;

        SOdialogueManager.Instance.StartDialogue(_dialogue);
    }

    private void OnDestroy()
    {
        if (_door != null)
        {
            _door.OnDoorOpened -= OnDoorOpened;
        }
    }
}
