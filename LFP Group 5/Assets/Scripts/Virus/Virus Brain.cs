using GridSystem;
using UnityEngine;

public class VirusBrain : MonoBehaviour
{
    //Block cells
    //Corrupt items
    //Move items
    //other stuff
    public void BlockCell(GridCell cellToBlock)
    {
        cellToBlock.SetIsBlocked(true);
    }
    public void CorruptItem(Item itemToCorrupt)
    {
        itemToCorrupt.SetIsCorrupt(true);
    }
    public void MoveItem(Item itemToMove, Vector2 newPosition)
    {
        itemToMove.transform.position = newPosition;
    }
}
