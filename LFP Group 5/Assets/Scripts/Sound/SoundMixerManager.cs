using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer = null;

    public void SetMasterVolume(float level)
    {
        //Takes log volume interpretation to linear interpretation
        float linearLevel = Mathf.Log10(level) * 20f;
        _audioMixer.SetFloat("MasterVolume",linearLevel);
    }
    public void SetSoundFXVolume(float level)
    {
        //Takes log volume interpretation to linear interpretation
        float linearLevel = Mathf.Log10(level) * 20f;
        _audioMixer.SetFloat("SoundFXVolume",linearLevel);
    }
    public void SetMusicVolume(float level)
    {
        //Takes log volume interpretation to linear interpretation
        float linearLevel = Mathf.Log10(level) * 20f;
        _audioMixer.SetFloat("MusicVolume",linearLevel);
    }
}
