using GridSystem;
using System.Collections.Generic;
using UnityEngine;

public class GridInteractable : MonoBehaviour
{
    // Stores every grid position occupied by the object (how many cells object takes up.
    private List<Vector2Int> _occupiedCells = new List<Vector2Int>();


    public IReadOnlyList<Vector2Int> _OccupiedCells => _occupiedCells;

    private void Start()
    {
        // works out which cells the object occupies.
        CalculateOccupiedCells();

        // tells the GridMap that those cells are blocked.
        BlockOccupiedCells();
    }

    private void CalculateOccupiedCells()
    {
        _occupiedCells.Clear();

        // Find the Collider2D attached to this obstacle.
        // The collider is used to determine the object's size.
        Collider2D _obstacleCollider = GetComponent<Collider2D>();

        if (_obstacleCollider == null)
        {
            Debug.LogWarning(
                $"{gameObject.name} has a GridObstacle but no Collider2D."
            );

            return;
        }

        // Get the world-space area covered by the collider.
        Bounds bounds = _obstacleCollider.bounds;

        // Find the minimum and maximum grid coordinates
        // covered by the collider.
        int minX = Mathf.FloorToInt(bounds.min.x);
        int maxX = Mathf.CeilToInt(bounds.max.x);

        int minY = Mathf.FloorToInt(bounds.min.y);
        int maxY = Mathf.CeilToInt(bounds.max.y);

        // Go through every grid position covered by the obstacle.
        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                // Add this cell to the list of occupied cells.
                _occupiedCells.Add(new Vector2Int(x, y));
            }
        }

        string occupiedCellText = string.Join(", ", _occupiedCells);

        Debug.Log($"{gameObject.name} occupies: {occupiedCellText}");
    }

    private void BlockOccupiedCells()
    {
        // Make sure a GridManager exists in the scene.
        if (GridManager.Instance == null)
        {
            Debug.LogWarning(
                $"{gameObject.name} could not find a GridManager."
            );

            return;
        }

        // Get the GridMap being used by the GridManager.
        GridMap gridMap = GridManager.Instance.GetGridMap();

        // Tell the GridMap that every cell occupied by this
        // obstacle should be blocked.
        foreach (Vector2Int cellPosition in _occupiedCells)
        {
            //gridMap.SetCellBlocked(cellPosition, true);
        }
    }
}
