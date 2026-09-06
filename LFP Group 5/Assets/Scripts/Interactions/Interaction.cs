using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private bool _canInteract = true;
    public virtual void Interact(GameObject interactingObject){}
    public virtual void SetCanInteract(bool newValue) => _canInteract = newValue;
}
