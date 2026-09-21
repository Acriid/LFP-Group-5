using System.Collections.Generic;
using GridSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "VirusBrain", menuName = "Virus/VirusBrain")]
public class VirusBrain : ScriptableObject
{
    //Block cells
    //Corrupt items
    //Move items
    //other stuff

    //Blocked cells
    //Item movement
    //



    public void BlockCell(GridCell cellToBlock)
    {
        cellToBlock.SetIsBlocked(true);
    }
    public void CorruptItem(Item itemToCorrupt)
    {
        itemToCorrupt.SetIsCorrupt(true);
    }
    public void MoveItem(Interaction itemToMove, Vector2 newPosition)
    {
        itemToMove.transform.position = newPosition;
    }

    public void BlockCells(List<Transform> cellBlockTransforms)
    {
        GridMap currentMap = GridManager.Instance.GetGridMap();
        foreach(GridCell gridCell in currentMap)
        {
            foreach(Transform blockTransform in cellBlockTransforms)
            {
                if(gridCell.ContainsPoint(blockTransform.position))
                {
                    BlockCell(gridCell);
                }
            }
        }
    }
}
