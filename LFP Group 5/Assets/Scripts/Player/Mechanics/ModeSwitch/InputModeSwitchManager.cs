using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputModeSwitchManager : MonoBehaviour
{
    [SerializeField] private ModeSwitchMechanic _modeSwitchMechanic = null;
    [SerializeField] private InputReader _inputReader = null;

    private List<InputAction> _actionsToRevert = new();

    void OnEnable()
    {
        _inputReader.GetDisabledActions();
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
        _actionsToRevert.Clear();
        foreach(InputAction action in _inputReader.GetDisabledActions())
        {
            _inputReader.EnableAction(action);
            _actionsToRevert.Add(action);
        }
    }

    private void NormalModeLogic()
    {
        foreach(InputAction action in _actionsToRevert)
        {
            _inputReader.DisableAction(action);
        }
        _actionsToRevert.Clear();
    }
}
