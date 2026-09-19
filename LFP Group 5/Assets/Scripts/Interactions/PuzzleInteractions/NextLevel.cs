using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : Interaction
{
    public override void Interact(GameObject interactingObject)
    {
        if (!_canInteract) return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
