using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
    public class DialogueCharacter
    {
        public Sprite icon;
    }

    [System.Serializable]
    public class DialogueLine
    {
        public DialogueCharacter character;
        [TextArea(3,10)]
        public string line;
    }

    [System.Serializable]
    public class Dialogue
    {
        public List<DialogueLine> dialogueLines = new List<DialogueLine>();
    }

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    [Header("Level 1 Dialogue After Movement")]
    public Dialogue movementDialogue;
    private bool movementDialoguePlayed = false;

    // initial dialogue for level start
    void Start()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void TriggerMovementDialogue()
    {
        if(movementDialoguePlayed)
        {
            return;
        }

        movementDialoguePlayed = true;
        DialogueManager.Instance.StartDialogue(movementDialogue);
    }
}
