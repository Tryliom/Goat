using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum SoundType
{
    None, GoatHurt, GoatWalk, ButtonClick, BackgroundAmbiance, ThrowChair, BreakChair, Death, ThrowBird, BreakBird,
    PillarUp, PillarDown, Bonus
}

[Serializable]
public class SoundClip
{
    [FormerlySerializedAs("Sound")] public SoundType SoundType;
    public AudioClip AudioClip;
    public float Volume = 1f;
}

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioData _audioData;
    
    [Header("Sounds")]
    [SerializeField] private List<SoundClip> _sounds;
    [SerializeField] private AudioSource _audioSource;
    
    public void PlaySound(SoundType soundType)
    {
        var soundClip = _sounds.Find(s => s.SoundType == soundType);
        
        if (soundClip == null) return;
        if (soundType == SoundType.None) return;

        _audioSource.PlayOneShot(soundClip.AudioClip, soundClip.Volume * _audioData.GetSoundVolume());
    }
}