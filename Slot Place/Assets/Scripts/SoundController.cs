using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    [SerializeField]
    private SwitchButton _buttonMusic;

    [SerializeField]
    private SwitchButton _buttonClip;

    [SerializeField]
    private AudioSource _backgroundMusic;

    public Action<bool> OnClipSoundChange;

    public Action<bool> OnMusicSoundChange;

    private PlayerData _playerData;

    [Inject] public void Initialize(PlayerData data)
    {
        _playerData = data;
        
        _backgroundMusic.Play();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (_buttonClip != null)
        {

            Instance.OnClipSoundChange += _buttonClip.SwitchSprite;

            Instance.OnMusicSoundChange += _buttonMusic.SwitchSprite;
        }

        OnClipSoundChange?.Invoke(FromIntToBool(data.VolumeClip));
        OnMusicSoundChange?.Invoke(FromIntToBool(data.VolumeMusic));

        _backgroundMusic.volume = data.VolumeMusic;
    }

    public void PlayAudioClip(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Vector2.zero, _playerData.VolumeClip);
    }

    public void SetSound(int soundType)
    {
        switch ((TypeSound)soundType)
        {
            case TypeSound.clip:
                {
                    SetMusicOpposite(ref _playerData.VolumeClip);
                    OnClipSoundChange?.Invoke(FromIntToBool(_playerData.VolumeClip));

                    break;
                }
            case TypeSound.music:
                {
                    SetMusicOpposite(ref _playerData.VolumeMusic);
                    _backgroundMusic.volume = _playerData.VolumeMusic;
                    OnMusicSoundChange?.Invoke(FromIntToBool(_playerData.VolumeMusic));
                    break;
                }
        }
    }

    public bool FromIntToBool(int i)
    {
        return i == 0 ? false : true;
    }


    public void SetMusicOpposite(ref int volume)
    {
        if (volume == 1)
        {
            volume = 0;
        }
        else
        {
            volume = 1;
        }

    }

}

public enum TypeSound
{
    clip,
    music
}
