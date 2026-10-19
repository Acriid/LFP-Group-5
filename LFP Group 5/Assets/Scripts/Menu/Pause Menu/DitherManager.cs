using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DitherManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = null;
    [SerializeField] private Material _ditherMaterial = null;
    [SerializeField] private List<Image> _imageList = new();
    [SerializeField] private float _ditherTime = 4f;

    private Coroutine _resetDitherRoutine = null;
    private float _elapsedTime = 0f;
    void OnEnable()
    {
        _inputReader.OnMove += DitherUI;
    }
    void OnDisable()
    {
        _inputReader.OnMove -= DitherUI;

        if(_resetDitherRoutine != null)
        {
            StopCoroutine(_resetDitherRoutine);
            _resetDitherRoutine = null;

            foreach(Image image in _imageList)
            {
                image.material = null;
            }
        }
    }

    private void DitherUI(Vector2Int moveInput)
    {
        if(moveInput == Vector2Int.zero) return;

        if(_resetDitherRoutine != null)
        {
            _elapsedTime = 0;
            return;
        }

        _resetDitherRoutine ??= StartCoroutine(ResetDither());

        foreach(Image image in _imageList)
        {
            image.material = _ditherMaterial;
        }
    }

    private IEnumerator ResetDither()
    {
        _elapsedTime = 0;
        while(_elapsedTime < _ditherTime)
        {
            _elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        foreach(Image image in _imageList)
        {
            image.material = null;
        }

        _resetDitherRoutine = null;
    }
}
