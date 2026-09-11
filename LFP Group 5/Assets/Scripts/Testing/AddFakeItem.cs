using UnityEngine;

public class AddFakeItem : MonoBehaviour
{
    public Inventory FakeInventory;
    public Item FakeItem;
    public void OnClick()
    {
        FakeInventory.AddToInventoryItem(FakeItem.gameObject);
    }
}
