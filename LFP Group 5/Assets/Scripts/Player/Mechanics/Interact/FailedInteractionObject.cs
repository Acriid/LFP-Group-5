using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FailedInteractionObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _textObject = null;
    public event Action<FailedInteractionObject> OnFinishMove;
    private bool _alreadyMoving = false;
    private Interaction _failedInteraction = null;
    public void MoveObject(Vector2 startPosition, Vector2 endPosition, float lifeTime)
    {
        if(_alreadyMoving) return;
        StartCoroutine(MoveObjectOverTime(startPosition,endPosition,lifeTime));
    }
    private IEnumerator MoveObjectOverTime(Vector2 startPosition, Vector2 endPosition, float lifeTime)
    {
        float elapsedTime = 0f;
        _alreadyMoving = true;

        while (elapsedTime < lifeTime)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / lifeTime);

            transform.position = Vector2.Lerp(
                startPosition,
                endPosition,
                t);

            yield return null;
        }

        transform.position = endPosition;
        _alreadyMoving = false;
        OnFinishMove?.Invoke(this);
    }
    public void SetFailedInteraction(Interaction newInteraction)
    {
        _failedInteraction = newInteraction;
    }
    public Interaction GetFailedInteraction()
    {
        return _failedInteraction;
    }
    public void SetText(string newText)
    {
        _textObject.text = newText;
    }
}
