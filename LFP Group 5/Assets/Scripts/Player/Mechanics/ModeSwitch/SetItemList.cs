using System.Collections.Generic;
using UnityEngine;

public class SetItemList : MonoBehaviour
{
    [SerializeField] private ItemModeSwitchManager _itemModeSwitchManager = null;
    [SerializeField] private List<Item> _itemList = new();

    void OnEnable()
    {
        if(_itemModeSwitchManager == null)
        {
            Debug.LogWarning("Item mode switch manager not set");
            return;
        }

        _itemModeSwitchManager.SetItemList(_itemList);
    }
}
