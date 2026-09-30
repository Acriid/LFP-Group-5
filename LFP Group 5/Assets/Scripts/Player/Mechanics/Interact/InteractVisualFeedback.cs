using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractVisualFeedback : MonoBehaviour
{
    [SerializeField] private InteractMechanic _interactMechanic = null;
    [SerializeField] private GameObject _feedBackUI = null;
    [SerializeField] private TMP_Text _feedBackText = null;
    private int _interactionsInRange = 0;
    private readonly WaitForSeconds _textUpdateWait = new(0.1f);
    private Coroutine _updateRoutine = null;

    private Interaction _targetInteraction = null;
    void OnEnable()
    {
        _interactMechanic.OnAddedInteraction += ShowInteraction;
    }
    void OnDisable()
    {
        _interactMechanic.OnAddedInteraction -= ShowInteraction;
    }
    private void ShowInteraction(int intValue)
    {
        _interactionsInRange += intValue;
        if(_interactionsInRange > 0)
        {
            if(_updateRoutine == null)
            {
                _updateRoutine = StartCoroutine(UpdateInteractionText());
                _feedBackUI.SetActive(true);
            }
        }
        else
        {
            if(_updateRoutine != null)
            {
                StopCoroutine(_updateRoutine);
                _updateRoutine = null;
                _feedBackUI.SetActive(false);

                _feedBackText.text = "";
            }           
        }
    }
    private IEnumerator UpdateInteractionText()
    {
        while(true)
        {
            _targetInteraction = _interactMechanic.GetTargetInteraction();
            if(_targetInteraction != null)
            _feedBackText.text = _targetInteraction.GetInteractionString();
            yield return _textUpdateWait;
        }
    }
}
