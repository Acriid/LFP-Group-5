using UnityEngine;

public class RemoveFakeItem : MonoBehaviour
{
    public Inventory FakeInventory;
    public InventoryUiManager FakeUiInventory;
    public Item FakeItem;
    public void OnClick()
    {
        FakeInventory.RemoveItemFromInventory(FakeUiInventory.GetCurrentSelectedItem().gameObject);
    }
}
