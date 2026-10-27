using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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


    public List<InputAction> DisableActionsList {get; private set;} = new();
    public InputReader InputReader {get; private set;} = null;
    public void SetInputReader(InputReader newInputReader)
    {
        InputReader = newInputReader;
    }
    public void SetDisableActionsList(List<InputAction> newList)
    {
        DisableActionsList = new(newList);
    }
    public void DisableActions()
    {
        if(InputReader == null) return;
        foreach(InputAction inputAction in DisableActionsList)
        {
            InputReader.DisableAction(inputAction);
        }
    }
    public void EnableActions()
    {
        if(InputReader == null) return;
        foreach(InputAction inputAction in DisableActionsList)
        {
            InputReader.EnableAction(inputAction);
        }        
    }

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
