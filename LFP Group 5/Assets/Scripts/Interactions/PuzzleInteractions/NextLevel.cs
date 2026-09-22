using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : Interaction
{
    [SerializeField] private int _nextLevel = -1;
    public override void Interact(GameObject interactingObject)
    {
        if (!_canInteract) return;

        if(_nextLevel == -1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        SceneManager.LoadScene(_nextLevel);
    }
}
