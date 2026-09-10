using Unity.VisualScripting;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private GameObject _heldObject = null;
    [SerializeField] private Vector2 _centerPosition = Vector2.zero;
    void Awake()
    {
        _centerPosition = transform.position;
    }
    public Vector2 GetCenterPosition()
    {
        return _centerPosition;
    }
    public void SetHeldObject(GameObject newObject, Transform newParent)
    {
        SetHeldObject(newObject);
        newObject.transform.SetParent(newParent);
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
