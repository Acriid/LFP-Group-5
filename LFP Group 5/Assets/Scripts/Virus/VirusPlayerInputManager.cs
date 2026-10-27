using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirusPlayerInputManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private InputReader _inputReader = null;
    [SerializeField] private VirusBrain _virusBrain = null;
    [Header("Inputs")]
    [SerializeField] private bool _disableMoveLeft = false;
    [SerializeField] private bool _disableMoveRight = false;
    [SerializeField] private bool _disableMoveUP = false;
    [SerializeField] private bool _disableMoveDown = false;

    private List<InputAction> _inputList = new();

    void OnEnable()
    {
        GetInputs();
    }
    private void GetInputs()
    {
        if(_inputReader == null) return;
        if(_virusBrain == null) return;
        _inputList.Clear();

        if(_disableMoveLeft)
        {
            _inputList.Add(_inputReader.GetMoveLeftAction);
        }
        if(_disableMoveRight)
        {
            _inputList.Add(_inputReader.GetMoveRightAction);
        }
        if(_disableMoveUP)
        {
            _inputList.Add(_inputReader.GetMoveUpAction);
        }
        if(_disableMoveDown)
        {
            _inputList.Add(_inputReader.GetMoveDownAction);
        }

        _virusBrain.SetDisableActionsList(_inputList);
        _virusBrain.SetInputReader(_inputReader);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _inputReader.EnableMoveActions();
            _virusBrain.DisableActions();
        }
    }
}
