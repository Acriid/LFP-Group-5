using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSet", menuName = "Audio/DefaultAudioSet")]
public class AudioSetScriptableObject : ScriptableObject
{
    public List<AudioClip> AudioList = new();
}
