using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnableVirus : MonoBehaviour
{
    [SerializeField] private GameObject _blockedCells, _virus, _nextVirus;
    [SerializeField] private Player _player;

    [SerializeField] private float _pixelationTime = 0.5f;

    void Awake()
    {
        if(_player == null)
        _player = FindAnyObjectByType<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        if (_player.GetCurrentMode() != GameMode.NormalMode) return;
 
        _virus.SetActive(true);
        _blockedCells.SetActive(true);
        
        if(PixelShaderManager.Instance == null) return;
        PixelShaderManager.Instance.PixelateScreen(_pixelationTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        if (_player.GetCurrentMode() != GameMode.NormalMode) return;

        if (_nextVirus != null)
        {
            _nextVirus.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
