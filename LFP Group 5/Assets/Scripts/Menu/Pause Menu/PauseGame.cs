using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = null;
    void OnEnable()
    {
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        _inputReader.EnableDropItemAction();
        _inputReader.EnableModeSwitchAction();
        
        Time.timeScale = 1;
    }
}
