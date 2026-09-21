using UnityEngine;

public class RoomInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _trigger;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _trigger.SetActive(true); ;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _trigger.SetActive(false);
        _collider.enabled = false;

    }
}
