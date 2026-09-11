using System.Collections.Generic;
using UnityEngine;

public class GridInteractable : MonoBehaviour
{
    private List<Vector2Int> occupiedCells = new List<Vector2Int>();

    public IReadOnlyList<Vector2Int> OccupiedCells => occupiedCells;

    private void Start()
    {
        CalculateOccupiedCells();
    }

    private void CalculateOccupiedCells()
    {
        occupiedCells.Clear();

        Collider2D _obstacleCollider = GetComponent<Collider2D>();

        if (_obstacleCollider == null)
        {
            Debug.LogWarning($"{gameObject.name} has a GridObstacle but no Collider2D.");
            return;
        }

        Bounds bounds = _obstacleCollider.bounds;

        int _minX = Mathf.FloorToInt(bounds.min.x);
        int _maxX = Mathf.CeilToInt(bounds.max.x);

        int _minY = Mathf.FloorToInt(bounds.min.y);
        int _maxY = Mathf.CeilToInt(bounds.max.y);

        for (int x = _minX; x < _maxX; x++)
        {
            for (int y = _minY; y < _maxY; y++)
            {
                occupiedCells.Add(new Vector2Int(x, y));
            }
        }

        Debug.Log(
            $"{gameObject.name} occupies {occupiedCells.Count} grid cells."
        );

        string _occupiedCellText = string.Join(", ", occupiedCells);

        Debug.Log($"{gameObject.name} occupies: {_occupiedCellText}");
    }
}
