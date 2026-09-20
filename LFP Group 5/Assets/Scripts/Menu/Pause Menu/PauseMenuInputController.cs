using Unity.VisualScripting;
using UnityEngine;

public class PauseMenuInputController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = null;
    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private bool _escapeEnabled = true;
    void OnEnable()
    {
        _inputReader.OnEscape += TogglePauseMenu;
    }
    void OnDisable()
    {
        _inputReader.OnEscape -= TogglePauseMenu;
    }
    private void TogglePauseMenu()
    {
        if(!_escapeEnabled) return;

        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
    }
    public void SetEscapeDisabled(bool newValue)
    {
        _escapeEnabled = newValue;
    }
}
