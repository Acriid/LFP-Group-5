using System.Collections.Generic;
using GridSystem;
using Unity.VisualScripting;
using UnityEngine;

public class VirusLevel2 : MonoBehaviour
{
    [SerializeField] private VirusBrain _virusBrain;
    [SerializeField] private List<Transform> _cellBlockTransforms;
    [SerializeField] private List<Item> _moveItems = new();
    [SerializeField] private List<Vector2> _moveItemPositions = new();

    void OnEnable()
    {
        _virusBrain.BlockCells(_cellBlockTransforms);

        for(int i = 0; i < _moveItems.Count ; i++)
        {
            _virusBrain.MoveItem(_moveItems[i],_moveItemPositions[i]);
        }
    }

}
