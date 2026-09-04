using GridSystem;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance {get; private set;}
    [SerializeField] private Transform _topLeft;
    [SerializeField] private Transform _bottomRight;
    [SerializeField] private int _cellSize = 1;
    private GridMap _gridMap = null;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;


        _gridMap = new(_topLeft,_bottomRight,_cellSize);
    }



    #region GetGridCell
    public GridCell GetGridCellAtPosition(GridCell referenceCell, Direction direction)
    {
        return _gridMap.GetGridCell(referenceCell,direction);
    }
    public GridCell GetGridCellAtPosition(GridCell referenceCell, Vector2Int offset)
    {
        return _gridMap.GetGridCell(referenceCell,offset);
    }
    public GridCell GetGridCellAtPosition(int column, int row)
    {
        return _gridMap.GetGridCell(column,row);
    }
    public GridCell GetGridCellAtPosition(Vector2Int cellPosition)
    {
        return _gridMap.GetGridCell(cellPosition.y,cellPosition.x);
    }
    #endregion

    public GridMap GetGridMap()
    {
        return _gridMap;
    }
}
