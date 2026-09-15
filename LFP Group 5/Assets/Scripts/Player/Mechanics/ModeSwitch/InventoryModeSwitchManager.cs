using Unity.VisualScripting;
using UnityEngine;

public class InventoryModeSwitchManager : MonoBehaviour
{
    [SerializeField] private ModeSwitchMechanic _modeSwitchMechanic = null;
    [SerializeField] private Inventory _playerInventory = null;
    [SerializeField] private int _safeModeMaxSize = 1;
    private int _originalMax = 0;
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
        _originalMax = _playerInventory.GetInventorySize();

        _playerInventory.SetMaxSize(_safeModeMaxSize);
    }
    private void NormalModeLogic()
    {
        _playerInventory.SetMaxSize(_originalMax);
    }
}
