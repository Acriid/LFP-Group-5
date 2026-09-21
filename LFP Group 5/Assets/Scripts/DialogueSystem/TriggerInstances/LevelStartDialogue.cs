using UnityEngine;

public class LevelStartDialogue : MonoBehaviour
{
    [SerializeField] private ScriptableObjectDialogu _dialogue;

    private void Start()
    {
        if (_dialogue != null && SOdialogueManager.Instance != null)
        {
            SOdialogueManager.Instance.StartDialogue(_dialogue);
        }
    }
    
}
