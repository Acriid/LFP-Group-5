using UnityEngine;

public class EnableVirus : MonoBehaviour
{
    [SerializeField] private GameObject _blockedCells, _virus;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _virus.SetActive(true);
        _blockedCells.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}
