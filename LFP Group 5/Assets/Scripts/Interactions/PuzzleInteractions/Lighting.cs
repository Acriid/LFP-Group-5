using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class Lighting : MonoBehaviour
{
    [SerializeField] private Light2D _roomLight;

    [Header("Brightness")]
    [SerializeField] private float _darkIntensity = 0f;
    [SerializeField] private float _brightIntensity = 1f;
    [SerializeField] private float _fadeDuration = 1f;

    private Coroutine _brightnessCoroutine;

    public void BrightenRoom()
    {
        if (_brightnessCoroutine != null)
        {
            StopCoroutine(_brightnessCoroutine);
        }

        _brightnessCoroutine = StartCoroutine(ChangeBrightness(_brightIntensity));
    }

    private IEnumerator ChangeBrightness(float targetIntensity)
    {
        float startIntensity = _roomLight.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / _fadeDuration;

            _roomLight.intensity = Mathf.Lerp(
                startIntensity,
                targetIntensity,
                t
            );

            yield return null;
        }

        _roomLight.intensity = targetIntensity;
    }
}
