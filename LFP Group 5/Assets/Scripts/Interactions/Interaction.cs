using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] protected bool _canInteract = true;
    [SerializeField] protected string _interactionString = "";
    [SerializeField] protected string _failedInteractionString = "";
    [Header("Audio")]
    [SerializeField] protected List<AudioClip> _interactionAudioList = new();
    [SerializeField] protected List<AudioClip> _failedInteractionAudioList = new();
    [Range(0f,1f)]
    [SerializeField] protected float _audioVolume = 1f;

    public event Action<string> OnFailedInteraction;
    public virtual void Interact(GameObject interactingObject)
    {
        PlayRandomInteractionSound();
    }

    public virtual void Interact(GameObject interactingObject, Item selectedItem)
    {
        PlayRandomInteractionSound();
    }
    public virtual void SetCanInteract(bool newValue) => _canInteract = newValue;

    public string GetInteractionString()
    {
        return _interactionString;
    }
    public bool GetCanInteract()
    {
        return _canInteract;
    }
    protected void InvokeFailedInteraction()
    {
        OnFailedInteraction?.Invoke(_failedInteractionString);
    }
    protected void InvokeFailedInteraction(string interactionString)
    {
        OnFailedInteraction?.Invoke(interactionString);
    }
    protected void PlayInteractionSound(AudioClip soundToPlay)
    {
        if(SoundFXManager.Instance == null) return;
        if(soundToPlay == null) return;

        SoundFXManager.Instance.PlaySoundFXClip(soundToPlay,transform,_audioVolume);
    }

    protected void PlayRandomInteractionSound()
    {
        if(SoundFXManager.Instance == null) return;
        if(_interactionAudioList.Count == 0) return;

        SoundFXManager.Instance.PlaySoundFXClip(_interactionAudioList,transform,_audioVolume);       
    }
    protected void PlayRandomFailedInteractionSound()
    {
        if(SoundFXManager.Instance == null) return;
        if(_failedInteractionAudioList.Count == 0) return;

        SoundFXManager.Instance.PlaySoundFXClip(_failedInteractionAudioList,transform,_audioVolume);        
    }
}
