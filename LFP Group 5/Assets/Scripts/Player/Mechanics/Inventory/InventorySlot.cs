using System;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private GameObject _heldObject = null;
    [SerializeField] private Image _backGroundImage = null;
    private Vector2 _centerPosition = Vector2.zero;

    private bool _isActive = true;
    public Action<InventorySlot> OnHeldObjectChange;

    private Color _originalColor;
    void Awake()
    {
        _originalColor = _backGroundImage.color;
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

            if(_heldObject.TryGetComponent(out UiItem uiItemComponent))
            {
                uiItemComponent.SetBackGroundImage(_backGroundImage);
            }
        }
        else
        {
            _backGroundImage.color = _originalColor;
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
