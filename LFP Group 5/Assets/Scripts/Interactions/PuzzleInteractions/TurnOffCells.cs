using UnityEngine;

public class TurnOffCells : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _blockedCells;
    void Update()
    {
        if (_player == null) return;

        if (_player.GetCurrentMode() != GameMode.NormalMode)
        {
            _blockedCells.SetActive(false);
        }

        if (_player.GetCurrentMode() == GameMode.NormalMode)
        {
            _blockedCells.SetActive(true);
        }
    }
}
