using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu = null;
    public void OnClick()
    {
        _pauseMenu.SetActive(false);
    }
}
