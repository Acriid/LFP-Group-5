using TMPro;
using UnityEngine;
using GridSystem;
using System.Collections;

public class TimerControlPanel : Interaction
{
    [Header("Timer Settings")]
    [SerializeField] private float _timeLimit = 2.5f;

    [Header("Timer UI")]
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private GameObject _timeLeft, _timeText, _startGate, _completeTrigger, _greenPanel;

    [Header("Puzzle Reset")]
    [SerializeField] private ItemPositionReset _itemPositionReset;

    private float _remainingTime;
    private bool _timerRunning = false;

    // Player checkpoint
    private Player _player;
    private GridCell _savedCell;

    private void Awake()
    {
        _remainingTime = _timeLimit;
        UpdateTimerUI();
    }

    public override void Interact(GameObject interactingObject)
    {
        if (!_canInteract) return;

        Player player = interactingObject.GetComponent<Player>();

        if (player == null)
        {
            Debug.LogWarning("Timer Control Panel could not find Player.");
            return;
        }

        // Save the player's current grid cell
        _player = player;
        _savedCell = player.GetCurrentCell();

        if (_savedCell == null)
        {
            Debug.LogWarning("Could not save player's grid cell.");
            return;
        }

        Debug.Log("PLAYER STARTING CELL SAVED: " + _savedCell.Center());

        StartTimer();

        base.Interact(interactingObject);
    }

    private void Update()
    {
        if (!_timerRunning) return;

        _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0f)
        {
            _remainingTime = 0f;
            _timerRunning = false;

            // Hide timer UI
            if (_timeLeft != null)
                _timeLeft.SetActive(false);

            if (_timeText != null)
                _timeText.SetActive(false);

            _startGate.SetActive(true);
            Debug.Log("TIME EXPIRED - PUZZLE FAILED");

            if (_itemPositionReset != null && _player != null)
            {
                _itemPositionReset.ResetItems(_player.GetInventory());
            }

            // Return player to saved grid cell
            ResetPlayerPosition();
        }

        UpdateTimerUI();
    }

    public void StartTimer()
    {
        _remainingTime = _timeLimit;
        _timerRunning = true;

        
        _startGate.SetActive(false);
        _completeTrigger.SetActive(true);

        // Show timer UI
        if (_timeLeft != null)
            _timeLeft.SetActive(true);

        if (_timeText != null)
            _timeText.SetActive(true);

        UpdateTimerUI();

        Debug.Log("TIMER STARTED: " + _remainingTime + " seconds");
    }

    private void ResetPlayerPosition()
    {
        if (_player == null || _savedCell == null)
        {
            Debug.LogWarning("Cannot reset player: Missing player or saved cell.");
            return;
        }

        _player.TeleportToCell(_savedCell);

        Debug.Log("PLAYER RETURNED TO STARTING CELL: " + _savedCell.Center());
    }

    private void UpdateTimerUI()
    {
        if (_timerText == null) return;

        _timerText.text = _remainingTime.ToString("F2") + "s";
    }

    public void StopTimer()
    {
        if (!_timerRunning) return;

        _timerRunning = false;

        _timeLeft.SetActive(false);
        _timeText.SetActive(false);

        StartCoroutine(CompleteTimedPuzzle());
        Debug.Log("PUZZLE COMPLETED - TIMER STOPPED");

        // Prevent the player from restarting this timer
        SetCanInteract(false);
    }

    private IEnumerator CompleteTimedPuzzle()
    {
        _greenPanel.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        _greenPanel.SetActive(false);
    }
}