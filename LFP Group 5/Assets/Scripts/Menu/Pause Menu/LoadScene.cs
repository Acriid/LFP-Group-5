using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{ 
    public void OnClick(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
