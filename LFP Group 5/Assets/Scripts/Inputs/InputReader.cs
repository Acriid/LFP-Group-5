using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Inputs/InputReader")]
public class InputReader : ScriptableObject
{
    #region InputAction Variables
    private PlayerInputs _playerInputs;   
    private InputAction _moveUpAction;
    private InputAction _moveDownAction;
    private InputAction _moveLeftAction;
    private InputAction _moveRightAction;


    private InputAction _interactAction;

    private InputAction _inventoryAction;

    private InputAction _modeSwitchAction;
    #endregion

    #region public Event Action Variables
    public event Action<Vector2Int> OnMove;

    public event Action OnInteract;

    public event Action OnInventory;

    public event Action OnModeSwitch;
    #endregion

    #region Current Direction Values
    private int _upValue;
    private int _downValue;
    private int _leftValue;
    private int _rightValue;
    #endregion

    #region Action Variables
    #region Move Variables
    private Action<InputAction.CallbackContext> _moveUpPerformed;
    private Action<InputAction.CallbackContext> _moveUpCanceled;

    private Action<InputAction.CallbackContext> _moveDownPerformed;
    private Action<InputAction.CallbackContext> _moveDownCanceled;

    private Action<InputAction.CallbackContext> _moveLeftPerformed;
    private Action<InputAction.CallbackContext> _moveLeftCanceled;

    private Action<InputAction.CallbackContext> _moveRightPerformed;
    private Action<InputAction.CallbackContext> _moveRightCanceled;
    #endregion
    private Action<InputAction.CallbackContext> _interactPerformed;
    private Action<InputAction.CallbackContext> _inventoryPerformed;
    private Action<InputAction.CallbackContext> _modeSwitchPerformed;
    #endregion
    #region Enable/Disable
    void OnEnable()
    {
        _playerInputs = new();

        InitializePlayerActions();
        InitializePlayerEvents();

        SubscribeActions();

    }
    void OnDisable()
    {
        UnSubscribeActions();
    }
    #endregion

    #region InitializeActions
    private void InitializePlayerActions()
    {
        //Move
        _moveUpAction = _playerInputs.Player.MoveUp;
        _moveDownAction = _playerInputs.Player.MoveDown;
        _moveLeftAction = _playerInputs.Player.MoveLeft;
        _moveRightAction = _playerInputs.Player.MoveRight;

        //Other
        _interactAction = _playerInputs.Player.Interact;

        _inventoryAction = _playerInputs.Player.Inventory;

        _modeSwitchAction = _playerInputs.Player.ModeSwitch;
    }
    #endregion

    #region Initialize Events
    private void InitializePlayerEvents()
    {
        //Move
        //Up is -1 due to (0,0) being the top left of the grid and
        //(x,y) being the bottom right of the grid.
        _moveUpPerformed = ctx => { _upValue = -1; RaiseMoveEvent(); };
        _moveUpCanceled = ctx => { _upValue = 0;  RaiseMoveEvent();};

        _moveDownPerformed = ctx => { _downValue = 1; RaiseMoveEvent(); };
        _moveDownCanceled = ctx => { _downValue = 0; RaiseMoveEvent(); };

        _moveLeftPerformed = ctx => { _leftValue = -1; RaiseMoveEvent(); };
        _moveLeftCanceled = ctx => { _leftValue = 0; RaiseMoveEvent(); };

        _moveRightPerformed = ctx => { _rightValue = 1; RaiseMoveEvent(); };
        _moveRightCanceled = ctx => { _rightValue = 0; RaiseMoveEvent(); };


        //Other
        _interactPerformed = ctx => OnInteract?.Invoke();

        _inventoryPerformed = ctx => OnInventory?.Invoke();

        _modeSwitchPerformed = ctx => OnModeSwitch?.Invoke();

    }
    #endregion


    #region Subscribe/Unsubscribe actions
    public void SubscribeActions()
    {
        //Move start
        _moveUpAction.performed += _moveUpPerformed;
        _moveUpAction.canceled += _moveUpCanceled;

        _moveDownAction.performed += _moveDownPerformed;
        _moveDownAction.canceled += _moveDownCanceled;

        _moveLeftAction.performed += _moveLeftPerformed;
        _moveLeftAction.canceled += _moveLeftCanceled;

        _moveRightAction.performed += _moveRightPerformed;
        _moveRightAction.canceled += _moveRightCanceled;
        //Move end

        //Other
        _interactAction.performed += _interactPerformed;

        _inventoryAction.performed += _inventoryPerformed;

        _modeSwitchAction.performed += _modeSwitchPerformed;
    }
    public void UnSubscribeActions()
    {
        //Move start
        _moveUpAction.performed -= _moveUpPerformed;
        _moveUpAction.canceled -= _moveUpCanceled;

        _moveDownAction.performed -= _moveDownPerformed;
        _moveDownAction.canceled -= _moveDownCanceled;

        _moveLeftAction.performed -= _moveLeftPerformed;
        _moveLeftAction.canceled -= _moveLeftCanceled;

        _moveRightAction.performed -= _moveRightPerformed;
        _moveRightAction.canceled -= _moveRightCanceled;
        //Move end

        //Other
        _interactAction.performed -= _interactPerformed;

        _inventoryAction.performed -= _inventoryPerformed;

        _modeSwitchAction.performed -= _modeSwitchPerformed;
    }
    #endregion

    #region Enable/DisableActions
    #region Move Action
    public void EnableMoveActions()
    {
        EnableMoveUpAction();
        EnableMoveDownAction();
        EnableMoveLeftAction();
        EnableMoveRightAction();
    }
    public void DisableMoveActions()
    {
        DisableMoveUpAction();
        DisableMoveDownAction();
        DisableMoveLeftAction();
        DisableMoveRightAction();        
    }
    public void EnableMoveUpAction()
    {
        _moveUpAction.Enable();
    }
    public void DisableMoveUpAction()
    {
        _moveUpAction.Disable();
        _upValue = 0;
        RaiseMoveEvent();
    }
    public void EnableMoveDownAction()
    {
        _moveDownAction.Enable();
    }
    public void DisableMoveDownAction()
    {
        _moveDownAction.Disable();
        _downValue = 0;
        RaiseMoveEvent();
    }
    public void EnableMoveLeftAction()
    {
        _moveLeftAction.Enable();
    }
    public void DisableMoveLeftAction()
    {
        _moveLeftAction.Disable();
        _leftValue = 0;
        RaiseMoveEvent();
    }
    public void EnableMoveRightAction()
    {
        _moveRightAction.Enable();
    }
    public void DisableMoveRightAction()
    {
        _moveRightAction.Disable();
        _rightValue = 0;
        RaiseMoveEvent();
    }
    #endregion
    public void EnableInteractAction()
    {
        _interactAction.Enable();
    }
    public void DisableInteractAction()
    {
        _interactAction.Disable();
    }
    public void EnableInventoryAction()
    {
        _inventoryAction.Enable();
    }
    public void DisableInventoryAction()
    {
        _inventoryAction.Disable();
    }
    public void EnableModeSwitchAction()
    {
        _modeSwitchAction.Enable();
    }
    public void DisableModeSwitchAction()
    {
        _modeSwitchAction.Disable();
    }
    #endregion
    #region Raise Combined Event
    private void RaiseMoveEvent()
    {
        int horizontal = _leftValue + _rightValue;
        int vertical = _upValue + _downValue;

        OnMove?.Invoke(new Vector2Int(horizontal, vertical));
    }
    #endregion

}