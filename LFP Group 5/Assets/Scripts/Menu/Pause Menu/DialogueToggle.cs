using UnityEngine;
using UnityEngine.UI;

public class DialogueToggle : MonoBehaviour
{
   [SerializeField] private Toggle dialogueToggle;
   
    private void Start()
    {
        // Dialogue is enabled by default
        dialogueToggle.isOn = true;

        dialogueToggle.onValueChanged.AddListener(SetDialogueEnabled);
    }

    private void SetDialogueEnabled(bool isEnabled)
    {
        if (SOdialogueManager.Instance == null)
            return;

        SOdialogueManager.Instance.SetDialogueEnabled(isEnabled);
    }

    private void OnDestroy()
    {
        if (dialogueToggle != null)
        {
            dialogueToggle.onValueChanged.RemoveListener(SetDialogueEnabled);
        }
    }
}
