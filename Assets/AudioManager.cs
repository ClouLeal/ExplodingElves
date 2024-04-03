using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum SoundType{
//     None,
//     Explosion,
//     Spawn,
// }

[Serializable]
public class Sound{

    [SerializeField] EffectType _type;
    public EffectType SoundType => _type;

    [SerializeField] AudioClip _clip;
    public AudioClip Clip => _clip;

    [SerializeField] float _mimDelayPlayAgaininSec;
    public float MimDelayPlaySoundAgain => _mimDelayPlayAgaininSec;

    [HideInInspector]
    public AudioSource AudioSource;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] _sounds;

    private Dictionary<EffectType, float>  _nextPlaySoundTime = new Dictionary<EffectType, float>();

    void Awake()
    {   foreach (var sound in _sounds)
        {
            sound.AudioSource = gameObject.AddComponent<AudioSource>();
            sound.AudioSource.clip = sound.Clip;
        }
    }

    public void Play(EffectType soundType)
    {
       
        if(_nextPlaySoundTime.ContainsKey(soundType) &&  Time.time < _nextPlaySoundTime[soundType]) return;

        Sound sound = Array.Find(_sounds, sound => sound.SoundType == soundType);
        if(sound == null) return;

        //Avoid polute audio if a lot of elfs explode
        if(_nextPlaySoundTime.ContainsKey(soundType))
        {
            _nextPlaySoundTime[soundType] = Time.time + sound.MimDelayPlaySoundAgain;
        }
        else
        {
             _nextPlaySoundTime.Add(soundType,Time.time + sound.MimDelayPlaySoundAgain);
        }

        sound.AudioSource.Play();
        
    }
}
