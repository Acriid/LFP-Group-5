using UnityEngine;

public class EnableVirus : MonoBehaviour
{
    [SerializeField] private GameObject _blockedCells, _virus;
    [SerializeField] private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (_player.GetCurrentMode() != GameMode.NormalMode) return;
 
            _virus.SetActive(true);
        _blockedCells.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       // if (_player.GetCurrentMode() != GameMode.NormalMode) return;
        gameObject.SetActive(false);
    }
}
