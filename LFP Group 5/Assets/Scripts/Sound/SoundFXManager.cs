using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance {get; private set;}

    [SerializeField] private AudioSource _soundFXObject = null;
    [SerializeField] private int _poolCount = 4;

    private GenericPool<AudioSource> _audioPool = null;


    void OnEnable()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;


        _audioPool = PoolManager.Instance.GetPool<AudioSource>(_soundFXObject.gameObject,_poolCount);
        if(_audioPool == null)
        {
            Debug.LogError("Failed to load bullet pool.");
        }


    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = _audioPool.Get();

        audioSource.transform.position = spawnTransform.position;

        audioSource.clip = audioClip;
        
        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        StartCoroutine(ReturnAfterTime(audioSource,clipLength));
    }

    public void PlaySoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        PlaySoundFXClip(audioClip[Random.Range(0,audioClip.Length)],spawnTransform,volume);
    }
    public void PlaySoundFXClip(List<AudioClip> audioClip, Transform spawnTransform, float volume)
    {
        PlaySoundFXClip(audioClip[Random.Range(0,audioClip.Count)],spawnTransform,volume);
    }

    private IEnumerator ReturnAfterTime(AudioSource sourceToReturn, float returnTime)
    {
        yield return new WaitForSeconds(returnTime);

        _audioPool.Return(sourceToReturn);
    }
}
