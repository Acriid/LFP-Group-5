using Unity.VisualScripting;
using UnityEngine;

public class MapModeSwitchManager : MonoBehaviour
{
    [SerializeField] private ModeSwitchMechanic _modeSwitchMechanic = null;
    [SerializeField] private GameObject _normalMap = null;
    [SerializeField] private GameObject _safeMap = null;
    void OnEnable()
    {
        _modeSwitchMechanic.OnModeSwitch += ManageInputMode;
    }
    void OnDisable()
    {
        _modeSwitchMechanic.OnModeSwitch -= ManageInputMode;
    }
    private void ManageInputMode(GameMode newMode)
    {
        if(newMode == GameMode.NormalMode)
        NormalModeLogic();
        else
        SafeModeLogic();
    }

    private void SafeModeLogic()
    {
        if(_normalMap != null)
        _normalMap.SetActive(false);
        if(_safeMap != null)
        _safeMap.SetActive(true);
    }
    private void NormalModeLogic()
    {
        if(_normalMap != null)
        _normalMap.SetActive(true);
        if(_safeMap != null)
        _safeMap.SetActive(false);
    }
}
