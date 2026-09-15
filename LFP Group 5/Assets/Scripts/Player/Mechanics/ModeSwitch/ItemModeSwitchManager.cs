using System.Collections.Generic;
using UnityEngine;

public class ItemModeSwitchManager : MonoBehaviour
{
    [SerializeField] private ModeSwitchMechanic _modeSwitchMechanic = null;
    [SerializeField] private List<Item> _itemList = new();
    private List<Item> _revertItemList = new();
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
        _revertItemList.Clear();

        foreach(Item item in _itemList)
        {
            if(item.GetIsCorrupt())
            {
                item.SetIsCorrupt(false);
                _revertItemList.Add(item);
            }
        }
    }
    private void NormalModeLogic()
    {
        foreach(Item item in _revertItemList)
        {
            item.SetIsCorrupt(true);
        }

        _revertItemList.Clear();
    }
}
