using UnityEngine;

public class CompletedTimedPuzzle : MonoBehaviour
{
    [SerializeField] private TimerControlPanel _controlPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _controlPanel.StopTimer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}
