using UnityEngine;

public class EnableVirus : MonoBehaviour
{
    [SerializeField] private GameObject _blockedCells, _virus;
    [SerializeField] private Player _player;

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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        if (_player.GetCurrentMode() != GameMode.NormalMode) return;
        gameObject.SetActive(false);
    }
}
