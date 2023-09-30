using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData", order = 1)]
public class AudioData : ScriptableObject
{
    [Range(0f, 1f)] public float MusicVolume = 1f;
    [Range(0f, 1f)] public float SoundVolume = 1f;
    [Range(0f, 1f)] public float GlobalVolume = 1f;
    
    public float GetMusicVolume()
    {
        return MusicVolume * GlobalVolume;
    }
    
    public float GetSoundVolume()
    {
        return SoundVolume * GlobalVolume;
    }
}