using UnityEngine;

public class CompletedTimedPuzzle : MonoBehaviour
{
    [SerializeField] private TimerControlPanel _controlPanel;
    [SerializeField] private GameObject _endLine;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _endLine.SetActive(false);
            _controlPanel.StopTimer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}
