using System;
using Unity.VisualScripting;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    [SerializeField] private GameObject _togglePanel = null;

    public void OnClick()
    {
        if(_togglePanel == null) return;

        _togglePanel.SetActive(!_togglePanel.activeSelf);
    }
}
