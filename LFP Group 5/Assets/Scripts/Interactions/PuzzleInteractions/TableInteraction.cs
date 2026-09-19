using UnityEngine;

public class TableInteraction : Interaction
{
    [SerializeField] private GameObject _hiddenInItem;

    public override void Interact(GameObject interactingObject)
    {
        if (!_canInteract) return;

        OpenDrawer();
    }

    private void OpenDrawer()
    {
        if (_hiddenInItem == null)
        {
            Debug.Log("No item found in the drawer");
            return;
        }

        _hiddenInItem.SetActive(true);
        SetCanInteract(false);
    }
}
