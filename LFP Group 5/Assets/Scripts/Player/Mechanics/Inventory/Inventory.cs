using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> _itemList;
    //When max items gets smaller items drop from back of the list.
    [SerializeField] private int _maxItems = 4;
    private int _currentItems = 0;
    //Should inventory keep track of the current item?

    void OnEnable()
    {
        _itemList = new(_maxItems);
    }

    public void ChangeMaxItems(int newValue)
    {
        _maxItems = newValue;

        //TODO- Change inventory to lock items
    }

    public void AddToInventoryItem(GameObject itemToAdd)
    {
        if(!itemToAdd.TryGetComponent(out Item itemComponent)) return;
        if(_currentItems == _maxItems) return;
        if(_itemList.Contains(itemComponent)) return;

        _itemList.Add(itemComponent);
        _currentItems++;
    }
}
