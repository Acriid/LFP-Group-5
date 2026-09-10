using UnityEngine;
using UnityEngine.EventSystems;

public class PanelHoverRelay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public System.Action OnPanelEnter;
    public System.Action OnPanelExit;

    public void OnPointerEnter(PointerEventData eventData) => OnPanelEnter?.Invoke();
    public void OnPointerExit(PointerEventData eventData) => OnPanelExit?.Invoke();
}
