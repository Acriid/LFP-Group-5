using System;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private GameObject _heldObject = null;
    private Vector2 _centerPosition = Vector2.zero;
    void Awake()
    {
        _centerPosition = ((RectTransform)transform).anchoredPosition;
    }
    public Vector2 GetCenterPosition()
    {
        return _centerPosition;
    }
    public void SetHeldObject(GameObject newObject, Transform newParent)
    {
        SetHeldObject(newObject);

        _heldObject.transform.SetParent(newParent,false);
    }
    public void SetHeldObject(GameObject newObject)
    {
        _heldObject = newObject;
        if(_heldObject == null) return;
        //Snap to center
        _heldObject.GetComponent<RectTransform>().anchoredPosition = _centerPosition;
    }
    public GameObject GetHeldObject()
    {
        return _heldObject;
    }

}
