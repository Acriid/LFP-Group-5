using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSet", menuName = "Audio/DefaultAudioSet")]
public class AudioSetScriptableObject : ScriptableObject
{
    public List<AudioClip> AudioList = new();
    public AudioClip GetRandomAudioClip()
    {
        if(AudioList.Count == 0) return null;

        return AudioList[Random.Range(0,AudioList.Count)];
    }
}
