using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ModeSwitch", menuName = "Mechanics/ModeSwitch")]
public class ModeSwitchMechanic : ScriptableObject
{
    public event Action<GameMode> OnModeSwitch;
    public void SwitchMode(GameMode newMode)
    {
        OnModeSwitch?.Invoke(newMode);
    }
}


public enum GameMode
{
    NormalMode,
    SafeMode
}
