using UnityEngine;

public class PlaySoundOnClick : MonoBehaviour
{
    [SerializeField] private AudioSetScriptableObject _audioSet = null;
    public void OnClick()
    {
        if(SoundFXManager.Instance == null) return;
        if(_audioSet == null) return;

        SoundFXManager.Instance.PlaySoundFXClip(_audioSet,transform,1f);
    }
}
