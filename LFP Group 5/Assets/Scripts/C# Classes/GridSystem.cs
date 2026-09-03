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
        public GridMap(Transform topLeft, Transform bottomRight, int cellSize)
        {
            BuildMap(topLeft,bottomRight,cellSize);
        }
        public GridMap(BoundsInt mapBounds, int cellSize = 0)
        {
            BuildMap(mapBounds,cellSize);
        }



        public void BuildMap(Transform topLeft, Transform bottomRight, int cellSize)
        {
            Vector2 minWorld = new(topLeft.position.x, bottomRight.position.y);
            Vector2 maxWorld = new(bottomRight.position.x, topLeft.position.y);

            Vector2Int minCell = new(Mathf.FloorToInt(minWorld.x), Mathf.FloorToInt(minWorld.y));
            Vector2Int maxCell = new(Mathf.CeilToInt(maxWorld.x), Mathf.CeilToInt(maxWorld.y));

            Vector2Int size = maxCell - minCell;
            BoundsInt mapBounds = new((Vector3Int)minCell, (Vector3Int)size);

            BuildMap(mapBounds, cellSize);
        }
        public void BuildMap(BoundsInt mapBounds, int cellSize)
        {
            if(cellSize == 0) return;


            int distanceBetween = mapBounds.max.x - mapBounds.min.x;
            int cellAmountX = distanceBetween/cellSize;

            distanceBetween = mapBounds.max.y - mapBounds.min.y;
            int cellAmountY = distanceBetween/cellSize;


            
            Vector2Int size = new(cellSize,cellSize);
            

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
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
