using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class GridCell
    {
        public GridCell(Vector3Int position, Vector3Int size)
        {
            GridBounds = new(position,size);
        }

        public BoundsInt GridBounds {get; private set;}

        public bool ContainsPoint(Vector2Int point)
        {
           return GridBounds.Contains((Vector3Int)point);
        }

        public void SetBounds(BoundsInt newBounds)
        {
            GridBounds = newBounds;
        }

        public Vector2 Center()
        {
            return GridBounds.center;
        }
    }

    public class GridMap
    {
        private List<List<GridCell>> _gridList;
        private Dictionary<GridCell, Vector2Int> _cellPositions;

        //Constructors
        public GridMap(Transform topLeft, Transform bottomRight, int cellSize)
        {
            BuildMap(topLeft,bottomRight,cellSize);
        }
        public GridMap(BoundsInt mapBounds, int cellSize = 0)
        {
            BuildMap(mapBounds,cellSize);
        }


        #region BuildMap
        /// <summary>
        /// Builds a GridMap to use for the level.
        /// </summary>
        /// <param name="topLeft">Top Left of the grid</param>
        /// <param name="bottomRight">Bottom Right of the grid</param>
        /// <param name="cellSize">Cell size per GridCell</param>
        public void BuildMap(Transform topLeft, Transform bottomRight, int cellSize)
        {
            //Get min and max points for BoundsInt
            Vector2 minWorld = new(topLeft.position.x, bottomRight.position.y);
            Vector2 maxWorld = new(bottomRight.position.x, topLeft.position.y);

            //Change min and max points to cell size
            Vector2Int minCell = new(Mathf.FloorToInt(minWorld.x), Mathf.FloorToInt(minWorld.y));
            Vector2Int maxCell = new(Mathf.CeilToInt(maxWorld.x), Mathf.CeilToInt(maxWorld.y));

            Vector2Int size = maxCell - minCell;
            BoundsInt mapBounds = new((Vector3Int)minCell, (Vector3Int)size);

            BuildMap(mapBounds, cellSize);
        }
        /// <summary>
        /// Builds a GridMap to use for the level.
        /// </summary>
        /// <param name="mapBounds">Given bounds for grid to be in</param>
        /// <param name="cellSize">Cell size per GridCells</param>
        public void BuildMap(BoundsInt mapBounds, int cellSize)
        {
            if(cellSize == 0) return;

            
            int distanceBetween = mapBounds.max.x - mapBounds.min.x;
            int cellAmountX = distanceBetween/cellSize;

            distanceBetween = mapBounds.max.y - mapBounds.min.y;
            int cellAmountY = distanceBetween/cellSize;


            
            Vector2Int size = new(cellSize,cellSize);
            

            //Populate grid list. [column][row]
            _gridList = new();

            for(int i = 0 ; i < cellAmountY ; i++)
            {
                Vector2Int currentCellMin = new(mapBounds.min.x,mapBounds.max.y - cellSize * (i+1));
                _gridList.Add(new());
                for(int j = 0 ; j < cellAmountX ; j++)
                {
                    GridCell currentCell = new((Vector3Int)currentCellMin,(Vector3Int)size);
                    _gridList[i].Add(currentCell);
                    currentCellMin.x += cellSize;
                }
            }

            if(_gridList.Count != 0)
            {
                BuildCellPositionLookup();
            }
        }
        /// <summary>
        /// Populate lookup dictionary to easily get specific cells.
        /// </summary>
        private void BuildCellPositionLookup()
        {
            _cellPositions = new Dictionary<GridCell, Vector2Int>();

            for(int i = 0; i < _gridList.Count; i++)
            {
                for(int j = 0; j < _gridList[i].Count; j++)
                {
                    _cellPositions[_gridList[i][j]] = new Vector2Int(j, i);
                }
            }
        }
        #endregion
        public void AddTestObjects(GameObject testObject)
        {
            foreach(List<GridCell> grids in _gridList)
            {
                foreach(GridCell gridCell in grids)
                {
                    GameObject.Instantiate(testObject,gridCell.Center(), Quaternion.identity);
                }
            }
        }


        #region GetGridCell
        /// <summary>
        /// Gets grid cell
        /// </summary>
        /// <param name="column">Column of cell</param>
        /// <param name="row">Row of cell</param>
        /// <returns></returns>
        public GridCell GetGridCell(int column, int row)
        {
            Vector2Int targetValue = new(column,row);
            GridCell targetCell = null;
            foreach(var pair in _cellPositions)
            {
                if(pair.Value == targetValue)
                {
                    targetCell = pair.Key;
                    break;
                }
            }

            return targetCell;
        }
        /// <summary>
        /// Gets grid cell
        /// </summary>
        /// <param name="referenceCell">Cell to start at</param>
        /// <param name="direction">Direction of new cell compared to reference</param>
        /// <returns></returns>
        public GridCell GetGridCell(GridCell referenceCell, Direction direction)
        {
            Vector2Int offset = direction switch
            {
                Direction.Up => new Vector2Int(0, -1),
                Direction.Down => new Vector2Int(0, 1),
                Direction.Left => new Vector2Int(-1, 0),
                Direction.Right => new Vector2Int(1, 0),
                _ => Vector2Int.zero
            };

            return GetGridCell(referenceCell, offset);
        }
        /// <summary>
        /// Gets grid cell
        /// </summary>
        /// <param name="referenceCell">Cell to start at</param>
        /// <param name="offset">Direction of new cell compared to reference</param>
        /// <returns></returns>
        public GridCell GetGridCell(GridCell referenceCell, Vector2Int offset)
        {
            if(!_cellPositions.TryGetValue(referenceCell, out Vector2Int position))
            {
                return null;
            }

            int targetX = position.x + offset.x;
            int targetY = position.y + offset.y;

            if(targetY < 0 || targetY >= _gridList.Count)
            {
                return null;
            }

            if(targetX < 0 || targetX >= _gridList[targetY].Count)
            {
                return null;
            }

            return _gridList[targetY][targetX];
        }
        #endregion
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
