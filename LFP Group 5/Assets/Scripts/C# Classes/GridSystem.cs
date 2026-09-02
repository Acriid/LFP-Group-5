using UnityEngine;

namespace GridSystem
{
    public class GridCell
    {
        public Bounds GridBounds {get; set;}

        public bool ContainsPoint(Vector2 point)
        {
           return GridBounds.Contains(point);
        }
    }

}
