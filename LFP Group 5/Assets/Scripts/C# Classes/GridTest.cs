using GridSystem;
using Unity.VisualScripting;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    [SerializeField] private Transform _topLeft;
    [SerializeField] private Transform _bottomRight;
    [SerializeField] private int _cellSize = 1;
    [SerializeField] private GameObject _testObject = null;

    void OnEnable()
    {
        GridMap gridMap = new(_topLeft,_bottomRight,_cellSize);
        gridMap.AddTestObjects(_testObject);
    }
}
