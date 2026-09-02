using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class GridCell
    {
        public Bounds GridBounds {get; private set;}

        public bool ContainsPoint(Vector2 point)
        {
           return GridBounds.Contains(point);
        }

        public void SetBounds(Bounds newBounds)
        {
            GridBounds = newBounds;
        }
    }

    public class GridMap
    {
        public List<List<GridCell>> GridList
        {
            get => _gridList;
            set
            {
                _gridList = value;
                BuildCellPositionLookup();
            }
        }

        private List<List<GridCell>> _gridList;
        private Dictionary<GridCell, Vector2Int> _cellPositions;

        public void BuildMap()
        {
            
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
