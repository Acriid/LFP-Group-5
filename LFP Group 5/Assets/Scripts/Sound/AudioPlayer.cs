using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip = null;
    [SerializeField] private List<AudioClip> _audioClips = new();
    public void PlayAudio()
    {
        if(SoundFXManager.Instance == null) return;
        SoundFXManager.Instance.PlaySoundFXClip(_audioClip,transform,1f);
    }

    public void PlayAudio(int audioIndex)
    {
        if(audioIndex >= _audioClips.Count || audioIndex < 0) return;


        SoundFXManager.Instance.PlaySoundFXClip(_audioClips[audioIndex],transform,1f);
    }

    public void PlayRandomAudio()
    {
        SoundFXManager.Instance.PlaySoundFXClip(_audioClips,transform,1f);
    }
}
