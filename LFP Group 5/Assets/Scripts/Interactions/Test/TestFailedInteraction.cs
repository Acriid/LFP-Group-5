using UnityEngine;
using System;

public class TestFailedInteraction : Interaction
{
    public override void Interact(GameObject interactingObject)
    {
        InvokeFailedInteraction();
    }
}
