using Unity.VisualScripting;
using UnityEngine;

public class TestPixelShader : MonoBehaviour
{
    [SerializeField] private PixelShaderManager _pixelShaderManager = null;

    void Start()
    {
        _pixelShaderManager.PixelateScreen(5f);
    }


}
