using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    [SerializeField] private GameObject _roomBackdrop, _outsideBackdrop, _trigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_roomBackdrop.activeInHierarchy)
        {
            _roomBackdrop.SetActive(false);
            _outsideBackdrop.SetActive(true);
        }
        else
        {
            _roomBackdrop.SetActive(true);
            _outsideBackdrop.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _trigger.SetActive(true);
        gameObject.SetActive(false);
    }
}
