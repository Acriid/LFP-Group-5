using System;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private GameObject _heldObject = null;
    private Vector2 _centerPosition = Vector2.zero;

    private bool _isActive = true;
    public Action<InventorySlot> OnHeldObjectChange;
    void Awake()
    {
        _centerPosition = ((RectTransform)transform).anchoredPosition;
    }
    public Vector2 GetCenterPosition()
    {
        return _centerPosition;
    }
    //TODO - Add event to change inventory slots
    public void SetHeldObject(GameObject newObject, Transform newParent)
    {
        SetHeldObject(newObject);

        _heldObject.transform.SetParent(newParent,false);
    }
    public void SetHeldObject(GameObject newObject)
    {
        _heldObject = newObject;
        if(_heldObject != null)
        {
            //Snap to center
            _heldObject.GetComponent<RectTransform>().anchoredPosition = _centerPosition;
        }

        
        OnHeldObjectChange?.Invoke(this);
    }
    public GameObject GetHeldObject()
    {
        return _heldObject;
    }

    public void SetIsActive(bool newValue)
    {
        _isActive = newValue;
    }
    public bool GetIsActive()
    {
        return _isActive;
    }

}
