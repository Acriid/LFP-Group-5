using System;
using Unity.VisualScripting;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] protected bool _canInteract = true;
    [SerializeField] protected string _interactionString = "";
    public virtual void Interact(GameObject interactingObject){}

    public virtual void Interact(GameObject interactingObject, Item selectedItem){}
    public virtual void SetCanInteract(bool newValue) => _canInteract = newValue;

    public string GetInteractionString()
    {
        return _interactionString;
    }
}
