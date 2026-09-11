using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/DefaultItem")]
public class ItemSO : ScriptableObject
{
    //To see if the player can use it in safe mode
    public bool IsActive = true;
    //To see if the virus has corrupted to item
    public bool IsCorrupt = false;
    public Sprite UISprite = null;
    public string ItemName = "";
    public string ItemDescription = "";
}
