using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicSource;
    public AudioSource SFXSource;

    public List<AudioClip> musicClipList;
    public List<AudioClip> SFXClipList;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitMusic();
        InitSFX();
    }

    private void InitMusic()
    {
        musicClipList = new List<AudioClip>();

        musicClipList.Add(Resources.Load<AudioClip>("Audio/Music/Confrontation in the Shadows"));
        musicClipList.Add(Resources.Load<AudioClip>("Audio/Music/Dangerous Scavenging"));
        musicClipList.Add(Resources.Load<AudioClip>("Audio/Music/Dark_Cave"));
        musicClipList.Add(Resources.Load<AudioClip>("Audio/Music/Desolation full loop"));
        musicClipList.Add(Resources.Load<AudioClip>("Audio/Music/LOOP_Stillness of Night"));
        musicClipList.Add(Resources.Load<AudioClip>("Audio/Music/So Delicate full loop"));
    }

    private void InitSFX()
    {
        SFXClipList = new List<AudioClip>();

        SFXClipList.Add(Resources.Load<AudioClip>("Audio/Effects/Coin"));
        SFXClipList.Add(Resources.Load<AudioClip>("Audio/Effects/Door_Break"));
        SFXClipList.Add(Resources.Load<AudioClip>("Audio/Effects/Hurt"));
        SFXClipList.Add(Resources.Load<AudioClip>("Audio/Effects/Jump"));
        SFXClipList.Add(Resources.Load<AudioClip>("Audio/Effects/Weapon_whoosh"));
        SFXClipList.Add(Resources.Load<AudioClip>("Audio/Effects/Footstep"));
        SFXClipList.Add(Resources.Load<AudioClip>("Audio/Effects/Hit"));
    }

    public void PlayMusic(MusicTracks track, float volume = 1.0f, bool loop = true)
    {
        musicSource.clip = musicClipList[(int)track];
        musicSource.volume = volume;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlaySFX(SFXList effect, float volume = 1.0f)
    {
        SFXSource.PlayOneShot(SFXClipList[(int)effect], volume);
    }
}

[System.Serializable]
public enum MusicTracks
{
    Confrontations_in_the_shadows,
    Dangerous_scavenging,
    Dark_cave,
    Desolation,
    Stillness_of_the_night,
    So_delicate
}

[System.Serializable]
public enum SFXList
{
    Coin,
    Door_break,
    Hurt,
    Jump,
    Weapon_Whoosh,
    Footstep,
    Hit
}
