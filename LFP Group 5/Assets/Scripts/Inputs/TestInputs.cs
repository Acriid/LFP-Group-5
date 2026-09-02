using UnityEngine;

public class TestInputs : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    void OnEnable()
    {
        _inputReader.OnMove += TestMove;
        _inputReader.EnableMoveActions();

        _inputReader.OnInteract += TestInteract;
        _inputReader.EnableInteractAction();

        _inputReader.OnInventory += TestInventory;
        _inputReader.EnableInventoryAction();

        _inputReader.OnModeSwitch += TestModeSwitch;
        _inputReader.EnableModeSwitchAction();
    }
    void OnDisable()
    {
        _inputReader.OnMove -= TestMove;
        _inputReader.DisableMoveActions();

        _inputReader.OnInteract -= TestInteract;
        _inputReader.DisableInteractAction();

        _inputReader.OnInventory -= TestInventory;
        _inputReader.DisableInventoryAction();

        _inputReader.OnModeSwitch -= TestModeSwitch;
        _inputReader.DisableModeSwitchAction();
    }

    private void TestMove(Vector2Int moveDirection)
    {
        Debug.Log(moveDirection);
    }
    private void TestInteract()
    {
        Debug.Log("Interacted");
    }
    private void TestInventory()
    {
        Debug.Log("Inventory");
    }
    private void TestModeSwitch()
    {
        Debug.Log("ModeSwitch");
    }
}
