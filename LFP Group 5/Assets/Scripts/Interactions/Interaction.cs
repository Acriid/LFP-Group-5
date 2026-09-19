using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] protected bool _canInteract = true;
    public virtual void Interact(GameObject interactingObject){}

    public virtual void Interact(GameObject interactingObject, Item selectedItem){}
    public virtual void SetCanInteract(bool newValue) => _canInteract = newValue;
}
