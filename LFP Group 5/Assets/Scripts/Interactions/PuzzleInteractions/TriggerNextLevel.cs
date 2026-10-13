using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextLevel : MonoBehaviour
{
    [SerializeField] private int _nextLevel = -1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_nextLevel == -1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(_nextLevel);
        }
    }
}
