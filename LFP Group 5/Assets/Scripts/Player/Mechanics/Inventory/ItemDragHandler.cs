using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _originalParent = null;
    private CanvasGroup _canvasGroup = null;
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalParent = transform.parent;
        transform.SetParent(transform.root);
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;


        InventorySlot dropSlot = eventData.pointerEnter != null ? eventData.pointerEnter.GetComponent<InventorySlot>() : null;
        if(!_originalParent.TryGetComponent(out InventorySlot originalSlot)) return;


        if(dropSlot == null)
        {
            GameObject item = eventData.pointerEnter;
            if(item != null)
            {
                dropSlot = item.GetComponent<InventorySlot>();
            }
        }

        if(dropSlot != null)
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
            transform.SetParent(_originalParent);
        }

        GetComponent<RectTransform>().anchoredPosition = originalSlot.GetCenterPosition();
    }
}
