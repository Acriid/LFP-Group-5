using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class SOdialogueManager : MonoBehaviour
{
    public static SOdialogueManager Instance;

    [Header("DialogueManager")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private Image _characterIcon;

    [SerializeField] private Animator _animator;

    [SerializeField] private float _typingSpeed = 0.03f;

    private ScriptableObjectDialogu _currentDialogue;
    private int _currentLineIndex;

    private Coroutine _typingCoroutine;

    private bool _isTyping;
    private string _currentSentence;

    public bool IsDialogueActive {get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _dialoguePanel.SetActive(false);
        _characterIcon.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!IsDialogueActive)
        {
            return;
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ContinueDialogue();
        }
    }

    public void StartDialogue(ScriptableObjectDialogu dialogue)
    {
        if (dialogue == null)
        {
            Debug.Log("SOdialogueManager: No dialogue rn");
            return;
        }

        if (dialogue.dialogueLines == null || dialogue.dialogueLines.Count == 0)
        {
            Debug.Log("SOdialogueManager: No dialogue lines");
            return;
        }

        _currentDialogue = dialogue;
        _currentLineIndex = 0;

        IsDialogueActive = true;
        _dialoguePanel.SetActive(true);
        _characterIcon.gameObject.SetActive(true);
        _animator.Play("show");

        DisplayCurrentLine();
    }

    private void DisplayCurrentLine()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
        }

        ScriptableObjectDialogu.DialogueLine line = _currentDialogue.dialogueLines[_currentLineIndex];
        
        _characterIcon.sprite = line._characterIcon;

        _currentSentence = line.line;
        _typingCoroutine = StartCoroutine(TypeSentence(_currentSentence));

    }

    private IEnumerator TypeSentence(string sentence)
    {
        _isTyping = true;
        _dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;

            yield return new WaitForSeconds(_typingSpeed);
        }

        _isTyping = false;
        _typingCoroutine = null;
    }

    private void ContinueDialogue()
    {
        if (!IsDialogueActive)
        {
            return;
        }

        if (_isTyping)
        {
            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
            }
            _dialogueText.text = _currentSentence;
            _isTyping = false;
            _typingCoroutine = null;

            return;
        }

        _currentLineIndex++;

        if (_currentLineIndex >= _currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
            return;
        }

        DisplayCurrentLine();
    }

    public void EndDialogue()
    {
        if (!IsDialogueActive)
            return;

        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
        }

        _isTyping = false;
        _typingCoroutine = null;

        IsDialogueActive = false;

        _currentDialogue = null;
        _currentSentence = "";

        _animator.Play("hide");
    }
}
