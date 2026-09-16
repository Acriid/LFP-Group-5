using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _originalParent = null;
    [SerializeField] private ItemSO _itemSO = null;
    [SerializeField] private CanvasGroup _canvasGroup = null;
    private Transform _dragParent = null;
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_itemSO == null) return;
        if(!_itemSO.IsActive) return;

        _originalParent = transform.parent;
        if(_dragParent != null)
        transform.SetParent(_dragParent);
        else
        transform.SetParent(transform.root);
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_itemSO == null) return;
        if(!_itemSO.IsActive) return;

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(_itemSO == null) return;
        if(!_itemSO.IsActive) return;

        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;


        InventorySlot dropSlot = eventData.pointerEnter != null ? eventData.pointerEnter.GetComponent<InventorySlot>() : null;
        if(!_originalParent.TryGetComponent(out InventorySlot originalSlot)) return;


        if(dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if(dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<InventorySlot>();
            }
        }

        if(dropSlot != null && dropSlot.GetIsActive())
        {
            if(dropSlot.GetHeldObject() != null)
            {
                originalSlot.SetHeldObject(dropSlot.GetHeldObject(),originalSlot.transform);
            }
            else
            {
                originalSlot.SetHeldObject(null);
            }

            dropSlot.SetHeldObject(gameObject,dropSlot.transform);
        }
        else
        {
            transform.SetParent(_originalParent,false);
            GetComponent<RectTransform>().anchoredPosition = originalSlot.GetCenterPosition();
        }

        
    }

    public void SetItemSO(ItemSO newItemSO)
    {
        _itemSO = newItemSO;
    }
    public void SetDragParent(Transform newParent)
    {
        _dragParent = newParent;
    }
}
