using System.Runtime.CompilerServices;
using GridSystem;
using UnityEngine;

public class GridModeSwitchManager : MonoBehaviour
{
    [SerializeField] private ModeSwitchMechanic _modeSwitchMechanic = null;
    private GridMap _originalMap = null;
    private GridMap _safeModeMap = null;
    void OnEnable()
    {
        _modeSwitchMechanic.OnModeSwitch += ManageGridMode;
    }
    void OnDisable()
    {
        _modeSwitchMechanic.OnModeSwitch -= ManageGridMode;
    }
    private void ManageGridMode(GameMode newMode)
    {
        if(newMode == GameMode.NormalMode)
        NormalModeLogic();
        else
        SafeModeLogic();
    }
    private void NormalModeLogic()
    {
        //Reset all Grid cells to their original state
        GridManager.Instance.SetGridMap(_originalMap);
    }
    private void SafeModeLogic()
    {
        //Open up all the grid cells
        _originalMap = new(GridManager.Instance.GetGridMap());

        //Initial creation of map
        //All cells should not be blocked
        if(_safeModeMap == null)
        {
            Debug.Log("Set SafeMap");
            _safeModeMap = new(_originalMap);
            foreach(GridCell gridCell in _safeModeMap)
            {
                if(gridCell.IsBlocked)
                {
                    gridCell.SetIsBlocked(false);
                }
            }
        }

        GridManager.Instance.SetGridMap(_safeModeMap);
    }
}
