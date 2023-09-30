using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum SoundType
{
    None, ProjectileHit, GoatHurt, GoatWalk, ButtonClick
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

    private readonly Dictionary<SoundType, float> _soundTimers = new();

    private void OnEnable()
    {
        foreach (var sound in Enum.GetValues(typeof(SoundType)))
        {
            _soundTimers.Add((SoundType) sound, 0);
        }
    }

    private void Update()
    {
        foreach (var sound in Enum.GetValues(typeof(SoundType)))
        {
            if (_soundTimers[(SoundType) sound] > 0)
            {
                _soundTimers[(SoundType) sound] -= Time.deltaTime;
            }
        }
    }
    
    public void PlaySound(SoundType soundType)
    {
        var soundClip = _sounds.Find(s => s.SoundType == soundType);
        
        if (soundClip == null) return;
        if (soundType == SoundType.None || _soundTimers[soundType] > 0) return;

        _audioSource.PlayOneShot(soundClip.AudioClip, soundClip.Volume * _audioData.GetSoundVolume());
        
        _soundTimers[soundType] = 0.1f;
    }
}