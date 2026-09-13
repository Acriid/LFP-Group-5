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
        _originalMap = GridManager.Instance.GetGridMap();

        if(_safeModeMap == null)
        {
            _safeModeMap = _originalMap;
            foreach(GridCell gridCell in _safeModeMap)
            {
                if(gridCell.IsBlocked)
                {
                    
                }
            }
        }
    }
}
