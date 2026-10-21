using UnityEngine;

public class PauseMenuInputController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = null;
    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private bool _escapeEnabled = true;
    void OnEnable()
    {
        _inputReader.OnEscape += TogglePauseMenu;
        _inputReader.EnableEscapeAction();
    }
    void OnDisable()
    {
        _inputReader.OnEscape -= TogglePauseMenu;
        _inputReader.DisableEscapeAction();
    }
    private void TogglePauseMenu()
    {
        if(!_escapeEnabled) return;

        Debug.Log("Toggled menu");
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);

        if(_pauseMenu.activeSelf)
        {
            _inputReader.DisableDropItemAction();
            _inputReader.DisableModeSwitchAction();
        }
        else
        {
            _inputReader.EnableDropItemAction();
            _inputReader.EnableModeSwitchAction();
        }
    }
    public void SetEscapeDisabled(bool newValue)
    {
        _escapeEnabled = newValue;
    }
}
