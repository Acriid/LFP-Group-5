using UnityEngine;

public class RoomInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _trigger;

    private void Awake()
    {
        _trigger.SetActive(true); ;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _trigger.SetActive(false);
    }
}
