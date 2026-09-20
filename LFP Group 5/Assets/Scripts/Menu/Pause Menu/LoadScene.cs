using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{ 
    [SerializeField] private bool _loadCurrentLevel = false;
    public void OnClick(int sceneToLoad)
    {
        if(_loadCurrentLevel)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
        SceneManager.LoadScene(sceneToLoad);
    }
}
