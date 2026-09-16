using System.Collections;
using GridSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Vector2Int _startPosition = Vector2Int.zero;
    [SerializeField] private float _timeBetweenMoves = 0.5f;
    [SerializeField] private Transform _itemParentTransform = null;
    [Header("Other Scripts")]
    [SerializeField] private InteractMechanic _interactMechanic = null;
    [SerializeField] private Inventory _inventory = null;

    [SerializeField] private ModeSwitchMechanic _modeSwitchMechanic = null;

    // REMOVE LATER
    public GameObject ModeSwitchPanel;
    // REMOVE LATER
    private GameMode _currentMode = GameMode.NormalMode;

    private GridCell _currentCell = null;
    private GridCell _nextCell = null;

    private float _moveCoolDown = 0f;
    private Vector2Int _moveInput;
    private bool _forceImmediateMove;

    void OnEnable()
    {
        //Gets first grid cell
        MovePlayer(_startPosition);

        StartCoroutine(CooldownClock());

        EnableInput();
    }
    void OnDisable()
    {
        DisableInput();
    }

    void Update()
    {
        MovePlayer(_moveInput);
    }
    private void ReadInput(Vector2Int moveInput)
    {
        if(moveInput != Vector2Int.zero && moveInput != _moveInput)
        {
            _forceImmediateMove = true;
        }
        _moveInput = moveInput;
    }

    private void MovePlayer(Vector2Int moveInput)
    {
        if(GridManager.Instance == null)
        {
            Debug.LogWarning("No GridManager Instance in scene, Player cannot move");
            return;
        }

        if(_moveCoolDown < _timeBetweenMoves && !_forceImmediateMove) return;

        //Get next cells
        if(_currentCell == null)
        {
            _nextCell = GridManager.Instance.GetGridCellAtPosition(moveInput);
        }
        else
        {
            _nextCell = GridManager.Instance.GetGridCellAtPosition(_currentCell,moveInput);
        }
        

        //Position to move to does not exist
        if(_nextCell == null) return;
        if(_nextCell.IsBlocked) return;

        _forceImmediateMove = false;

        transform.position = _nextCell.Center();
        _currentCell = _nextCell;
        _nextCell = null;

        StartCoroutine(CooldownClock());
    }


    private IEnumerator CooldownClock()
    {
        //Start move Cooldown
        _moveCoolDown = 0f;
        while(_moveCoolDown < _timeBetweenMoves)
        {
            _moveCoolDown += Time.deltaTime;
            yield return null;
        }
    }

    private void Interact()
    {
        if(_interactMechanic == null) return;
        GameObject interactionObject = _interactMechanic.GetTargetInteractionGameObject();
        _interactMechanic.Interact(gameObject);
        if(_inventory == null) return;
        _inventory.AddToInventoryItem(interactionObject);
    }
    private void DropSelectedItem()
    {
        GameObject item = _inventory.RemoveItemFromInventory();

        item.SetActive(true);
        item.transform.SetParent(_itemParentTransform);
        //For now item is set beneath the player.
        item.transform.position = transform.position;
    }
    private void SwitchMode()
    {
        if(_currentMode == GameMode.NormalMode)
        {
            _currentMode = GameMode.SafeMode;
            //REMOVE LATER
            ModeSwitchPanel.SetActive(true);
        }
        else
        {
            _currentMode = GameMode.NormalMode;
            //REMOVE LATER
            ModeSwitchPanel.SetActive(false);
        }

        _modeSwitchMechanic.SwitchMode(_currentMode);
    }

    private void EnableInput()
    {
        _inputReader.OnMove += ReadInput;
        _inputReader.EnableMoveActions();      

        _inputReader.OnInteract += Interact;
        _inputReader.EnableInteractAction();

        _inputReader.OnDropItem += DropSelectedItem;
        _inputReader.EnableDropItemAction();

        _inputReader.OnModeSwitch += SwitchMode;
        _inputReader.EnableModeSwitchAction();
    }
    private void DisableInput()
    {
        _inputReader.OnMove -= ReadInput;
        _inputReader.DisableMoveActions(); 

        _inputReader.OnInteract -= Interact;
        _inputReader.DisableInteractAction();

        _inputReader.OnDropItem -= DropSelectedItem;
        _inputReader.DisableDropItemAction();    

        _inputReader.OnModeSwitch -= SwitchMode;  
        _inputReader.DisableModeSwitchAction();
    }
}
