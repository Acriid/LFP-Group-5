using UnityEngine;

public class GateDiaogueTrigger : MonoBehaviour
{
    [SerializeField] private PressurePlate _pressurePlate;
    [SerializeField] private ScriptableObjectDialogu _dialogue;

    private bool _hasTriggered;

    private void Start()
    {
        if (_pressurePlate == null)
        {
            Debug.LogWarning("GateDialogueTrigger: No PressurePlate assigned.");
            return;
        }

        _pressurePlate.OnGateOpened += OnGateOpened;
    }

    private void OnGateOpened()
    {
        if (_hasTriggered)
            return;

        if (_dialogue == null)
        {
            Debug.LogWarning("GateDialogueTrigger: No dialogue SO assigned.");
            return;
        }

        if (SOdialogueManager.Instance == null)
        {
            Debug.LogWarning("GateDialogueTrigger: No SOdialogueManager found.");
            return;
        }

        _hasTriggered = true;

        SOdialogueManager.Instance.StartDialogue(_dialogue);
    }

    private void OnDestroy()
    {
        if (_pressurePlate != null)
        {
            _pressurePlate.OnGateOpened -= OnGateOpened;
        }
    }
}
