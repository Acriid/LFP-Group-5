using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CoroutineDialogue : MonoBehaviour
{
    [Header("Dialogues")]
    [SerializeField] private ScriptableObjectDialogu _firstDialogue;
    [SerializeField] private ScriptableObjectDialogu _secondDialogue;

    [Header("Delay Settings")]
    [SerializeField] private float _delayBetweenDialogues = 5f;
    [SerializeField] private float _menuTransitionDelay = 2f;

    [Header("Scene Settings")]
    [SerializeField] private string _mainMenuSceneName = "MainMenu";

    private void Start()
    {
        if (_firstDialogue != null && SOdialogueManager.Instance != null)
        {
            SOdialogueManager.Instance.StartDialogue(_firstDialogue);

            StartCoroutine(WaitForNextDialogue());
        }
    }

    private IEnumerator WaitForNextDialogue()
    {
        // Wait until the first dialogue has finished
        yield return new WaitUntil(() =>
            !SOdialogueManager.Instance.IsDialogueActive);

        // Wait 5 seconds after the dialogue finishes
        yield return new WaitForSeconds(_delayBetweenDialogues);

        // Trigger the second dialogue
        if (_secondDialogue != null)
        {
            SOdialogueManager.Instance.StartDialogue(_secondDialogue);
        }

        yield return new WaitUntil(() =>
            !SOdialogueManager.Instance.IsDialogueActive);

        // Brief pause before returning to menu
        yield return new WaitForSeconds(_menuTransitionDelay);

        // Load Main Menu
        SceneManager.LoadScene(_mainMenuSceneName);
    }
}