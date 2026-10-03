using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PixelShaderManager : MonoBehaviour
{
    [SerializeField] private Renderer2DData _renderer2DData = null;
    [SerializeField] private Material _pixelMaterial = null;

    private FullScreenPassRendererFeature _fullScreenPass = null;

    private void Awake()
    {
        FindFullScreenPass();
    }
    private void FindFullScreenPass()
    {
        foreach (var feature in _renderer2DData.rendererFeatures)
        {
            if (feature is FullScreenPassRendererFeature fullScreenPass)
            {
                _fullScreenPass = fullScreenPass;
                break;
            }
        }       
    }

    public void PixelateScreen(bool pixelate)
    {
        if(_fullScreenPass == null) return;

        if(pixelate)
        _fullScreenPass.passMaterial = _pixelMaterial;
        else
        _fullScreenPass.passMaterial = null;
    }
    public void PixelateScreen(float pixelateTime)
    {
        StartCoroutine(PixelateForSpecificTime(pixelateTime));
    }
    private IEnumerator PixelateForSpecificTime(float pixelateTime)
    {
        PixelateScreen(true);
        yield return new WaitForSeconds(pixelateTime);
        PixelateScreen(false);
    }
}
