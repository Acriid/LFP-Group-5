using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FailedInteractionFeedback : MonoBehaviour
{
    [SerializeField] private InteractMechanic _interactMechanic = null;
    [SerializeField] private GameObject _failedInteractObject = null;
    [SerializeField] private int _failedInteractPoolSize = 5;
    [SerializeField] private float _failedMoveDistance = 3f;
    [SerializeField] private float _failedLifetime = 3f;
    private GenericPool<FailedInteractionObject> _failedInteractionPool;
    private List<Interaction> _currentTextList = new();
    private Interaction _lastInteraction = null;
    void OnEnable()
    {
        _interactMechanic.OnInteraction += InitializeInteraction;


        _failedInteractionPool = PoolManager.Instance.GetPool<FailedInteractionObject>(_failedInteractObject,_failedInteractPoolSize);
        if(_failedInteractionPool == null)
        {
            Debug.LogError("Failed to load uiItem pool.");
        }
    }
    void OnDisable()
    {
        _interactMechanic.OnInteraction -= InitializeInteraction;
    }
    private void InitializeInteraction(Interaction targetInteraction)
    {
        targetInteraction.OnFailedInteraction += ShowFailedInteraction;
        _lastInteraction = targetInteraction;
    }
    private void ShowFailedInteraction(string failedText)
    {

        _lastInteraction.OnFailedInteraction -= ShowFailedInteraction;

        
        if(_currentTextList.Contains(_lastInteraction)) return;

        FailedInteractionObject interactionInstance = _failedInteractionPool.Get();

        _currentTextList.Add(_lastInteraction);
        interactionInstance.SetFailedInteraction(_lastInteraction);

        interactionInstance.OnFinishMove += ReturnObject;

        if(failedText != "")
        interactionInstance.SetText(failedText);
        else
        interactionInstance.SetText("Cannot interact (ADD TEXT)");

        Vector2 startPosition = _lastInteraction.gameObject.transform.position;
        Vector2 endPosition = startPosition + new Vector2(0f,_failedMoveDistance);

        interactionInstance.MoveObject(startPosition,endPosition,_failedLifetime);
    }
    private void ReturnObject(FailedInteractionObject objectToReturn)
    {
        objectToReturn.OnFinishMove -= ReturnObject;

        _currentTextList.Remove(objectToReturn.GetFailedInteraction());

        _failedInteractionPool.Return(objectToReturn);
    }
}
