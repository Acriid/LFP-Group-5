using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _informationPanel = null;
    private bool _isHoveringItem = false;
    private bool _isHoveringPanel = false;

    private void Awake()
    {
        if (_informationPanel != null)
        {

            if(_informationPanel.TryGetComponent(out PanelHoverRelay panelHover))
            panelHover = _informationPanel.AddComponent<PanelHoverRelay>();

            panelHover.OnPanelEnter += () => { _isHoveringPanel = true; };
            panelHover.OnPanelExit += () => { _isHoveringPanel = false; UpdatePanelVisibility(); };
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHoveringItem = true;
        ShowInformationPanel();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _isHoveringItem = false;
        UpdatePanelVisibility();
    }
    private void ShowInformationPanel()
    {
        if(_informationPanel != null)
        _informationPanel.SetActive(true);
    }
    private void UpdatePanelVisibility()
    {
        if (_informationPanel != null && !_isHoveringItem && !_isHoveringPanel)
            _informationPanel.SetActive(false);
    }
}
