
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class GridMapTest : MonoBehaviour
{
    void Start()
    {
        GridMap gridMap1 = GridManager.Instance.GetGridMap();
        GridMap gridMap2 = new(gridMap1);

        foreach(GridCell gridCell in gridMap2)
        {
            gridCell.SetIsBlocked(true);
        }

        Debug.Log(gridMap1==gridMap2);

        List<GameObject> testList1 = new()
        {
            gameObject
        }; 

        List<GameObject> testList2 = new(testList1);
        testList2.Remove(gameObject);

        Debug.Log(testList1==testList2);
    }

}
