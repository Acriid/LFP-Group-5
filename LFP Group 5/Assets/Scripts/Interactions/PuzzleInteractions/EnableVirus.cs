using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnableVirus : MonoBehaviour
{
    [SerializeField] private GameObject _blockedCells, _virus, _nextVirus;
    [SerializeField] private Player _player;

    [SerializeField] private float _pixelationTime = 0.5f;
    [SerializeField] private AudioSetScriptableObject _audioSet = null;

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

        if(SoundFXManager.Instance != null && _audioSet != null)
        SoundFXManager.Instance.PlaySoundFXClip(_audioSet,transform,1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_virus.activeInHierarchy)
        {
            _virus.SetActive(true);
        }
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
