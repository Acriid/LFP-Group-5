using Unity.VisualScripting;
using UnityEngine;

public class InventoryModeSwitchManager : MonoBehaviour
{
    [SerializeField] private ModeSwitchMechanic _modeSwitchMechanic = null;
    [SerializeField] private Inventory _playerInventory = null;
    [SerializeField] private InventoryUiManager _playerUIInventory = null;
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
        _playerUIInventory.SetSafeModeSprite();


        _originalMax = _playerInventory.GetInventorySize();

        _playerInventory.SetAllowedSize(_safeModeMaxSize);
    }
    private void NormalModeLogic()
    {
        _playerUIInventory.SetNormalModeSprite();

        _playerInventory.SetAllowedSize(_originalMax);
    }
}
