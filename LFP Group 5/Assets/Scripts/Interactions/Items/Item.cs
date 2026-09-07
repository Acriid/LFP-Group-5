using UnityEngine;

public class Item : Interaction
{
    public override void Interact(GameObject interactingObject)
    {
        if(!_canInteract) return;

        PickUpItem(interactingObject);
    }
    private void PickUpItem(GameObject interactingObject)
    {
        //TODO - Take object from game world and add it to ui
    }
}
