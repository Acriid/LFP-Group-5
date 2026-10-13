using UnityEngine;
using UnityEngine.UI;

public class DialogueToggle : MonoBehaviour
{
   [SerializeField] private Toggle dialogueToggle;
   
   private const string DialogueKey = "DialogueEnabled";
   
    private void Start()
    {
        // load in what the dialoguekey is on. Dialogue starts on
        bool dialogueEnabled = PlayerPrefs.GetInt(DialogueKey, 1) == 1;

        dialogueToggle.SetIsOnWithoutNotify(dialogueEnabled);
        
        if (SOdialogueManager.Instance != null)
            {
                SOdialogueManager.Instance.SetDialogueEnabled(dialogueEnabled);
            }

        dialogueToggle.onValueChanged.AddListener(SetDialogueEnabled);
    }

    private void SetDialogueEnabled(bool isEnabled)
    {
        PlayerPrefs.SetInt(DialogueKey, isEnabled ? 1 : 0);
        PlayerPrefs.Save();
        
        
        if (SOdialogueManager.Instance != null)
        {
            SOdialogueManager.Instance.SetDialogueEnabled(isEnabled);
        }
    }

    private void OnDestroy()
    {
        if (dialogueToggle != null)
        {
            dialogueToggle.onValueChanged.RemoveListener(SetDialogueEnabled);
        }
    }
}
