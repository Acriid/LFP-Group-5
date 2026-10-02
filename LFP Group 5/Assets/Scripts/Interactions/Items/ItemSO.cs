using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/DefaultItem")]
public class ItemSO : ScriptableObject
{
    //To see if the player can use it in safe mode
    
    public event Action<bool> OnIsActiveChange;
    private bool _isActive = true;
    public bool IsActive
    {
        get => _isActive;
        set 
        {
            if(_isActive != value)
            {
                _isActive = value;
                OnIsActiveChange?.Invoke(_isActive);
            }
        }
    }
    

    

    //To see if the virus has corrupted to item
    public bool IsCorrupt = false;
    
    public bool IsSelected = false;


    public Sprite NormalModeSprite = null;
    public Sprite SafeModeSprite = null;



    //UI Item Variables
    public Sprite UISpriteNormal = null;
    public Sprite UISpriteSafe = null;
    public string ItemName = "";
    public string ItemDescription = "";
}
