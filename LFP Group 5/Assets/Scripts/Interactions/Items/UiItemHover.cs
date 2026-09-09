using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiItemHover : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject _descriptionPanel = null;
    //Requires the EventSystem in the scene.
    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
