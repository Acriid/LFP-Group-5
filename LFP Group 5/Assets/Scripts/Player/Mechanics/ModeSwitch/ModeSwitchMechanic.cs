using System;
using UnityEngine;

public class ModeSwitchMechanic : MonoBehaviour
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
