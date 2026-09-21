using UnityEngine;
using GridSystem;

public class PressurePlate : Interaction
{
    [SerializeField] private ItemSO _requiredItem;
    private bool _isActivated = false;
    [SerializeField] private GameObject _gate;

    private void Update()
    {
        CheckPlate();
    }

    public void CheckPlate()
    {
        // Find the grid cell closest to the pressure plate
        GridCell plateCell = GetNearestGridCell(transform.position);

        if (plateCell == null)
            return;

        // Find all active Items in the scene
        Item[] items = FindObjectsByType<Item>(FindObjectsSortMode.None);

        foreach (Item item in items)
        {
            // Find the grid cell closest to the item
            GridCell itemCell = GetNearestGridCell(item.transform.position);

            if (itemCell == null)
                continue;

            // Check if the item is occupying the same cell as the pressure plate
            if (itemCell.Equals(plateCell))
            {
                if (item.GetItemSO() == _requiredItem)
                {
                    ActivatePlate();
                    return;
                }
            }
        }

        // No item is on the pressure plate
        DeactivatePlate();
    }

    private GridCell GetNearestGridCell(Vector2 worldPosition)
    {
        GridMap gridMap = GridManager.Instance.GetGridMap();

        if (gridMap == null)
            return null;

        GridCell nearestCell = null;
        float closestDistance = Mathf.Infinity;

        foreach (GridCell cell in gridMap)
        {
            float distance = Vector2.Distance(
                worldPosition,
                cell.Center()
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestCell = cell;
            }
        }

        return nearestCell;
    }

    private void ActivatePlate()
    {
        if (_isActivated)
            return;

        _isActivated = true;

        if (_gate == null) return;
        _gate.SetActive(false);
        Debug.Log("PRESSURE PLATE ACTIVATED!");
    }

    private void DeactivatePlate()
    {
        if (!_isActivated)
            return;

        _isActivated = false;

        if (_gate == null) return;
        _gate.SetActive(true);
        Debug.Log("PRESSURE PLATE DEACTIVATED!");
    }
}