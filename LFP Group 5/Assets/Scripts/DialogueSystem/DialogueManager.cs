using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    
    public Image characterIcon;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines = new Queue<DialogueLine>();

    public bool isDialogueActive = false;
    public float typingSpeed = 0.1f;
    
    public Animator animator;

    public bool isTyping = false;
    public string currentSentence;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        animator.Play("show");
        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (!isDialogueActive)
        {
            return;
        }
        
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueArea.text = currentSentence;
            isTyping = false;
            return;
        }
        
        
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;

        //StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        isTyping = true;
        currentSentence = dialogueLine.line;
        
        dialogueArea.text = " ";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        StopAllCoroutines();
        animator.Play("hide");
    }

    void Update()
    {
        if (isDialogueActive && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            DisplayNextDialogueLine();
        }
    }
}
