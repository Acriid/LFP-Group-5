using System.Collections.Generic;
using UnityEngine;

public class ItemModeSwitchManager : MonoBehaviour
{
    [SerializeField] private ModeSwitchMechanic _modeSwitchMechanic = null;
    [SerializeField] private List<Item> _itemList = new();
    private List<Item> _revertCorruptionItemList = new();
    private List<Item> _implementCorruptionItemList = new();
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
        _revertCorruptionItemList.Clear();
        _implementCorruptionItemList.Clear();

        foreach(Item item in _itemList)
        {
            if(item == null) continue;
            if(item.GetIsCorrupt())
            {
                item.SetIsCorrupt(false);
                _revertCorruptionItemList.Add(item);
            }
            else
            {
                item.SetIsCorrupt(true);
                _implementCorruptionItemList.Add(item);
            }
        }
    }
    private void NormalModeLogic()
    {
        foreach(Item item in _revertCorruptionItemList)
        {
            item.SetIsCorrupt(true);
        }

        _revertCorruptionItemList.Clear();

        foreach(Item item in _implementCorruptionItemList)
        {
            item.SetIsCorrupt(false);
        }

        _implementCorruptionItemList.Clear();
    }
    public void SetItemList(List<Item> newList)
    {
        _itemList = new(newList);
    }
    void OnDestroy()
    {
        NormalModeLogic();
    }
}
