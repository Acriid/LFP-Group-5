using UnityEngine;

public class TurnOffCells : MonoBehaviour
{
    [SerializeField] private GameObject _blockedCells;
    [SerializeField] private ModeSwitchMechanic _modeSwitchMechanic = null;
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
        _blockedCells.SetActive(false);
    }
    private void NormalModeLogic()
    {
        _blockedCells.SetActive(true);
    }
}
