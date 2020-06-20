using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsHelper : MonoBehaviour
{
    /// Singleton

    public static SoundEffectsHelper Instance;

    public AudioClip[] explosionSounds;
    public AudioClip[] underwaterExplosionSounds;
    public AudioClip[] playerShotSounds;
    public AudioClip[] hitHurtSounds;
    public float playerShotVolume = 0.4f;
    private float hitHurtVolume = 1f;
    private AudioSource MusicPlayer;


    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        Instance = this;
    }

    void Start()
    {
        MusicPlayer = GetComponent<AudioSource>();
    }

    public void MakeExplosionSound(float volume)
    {
        var i = Random.Range(0, (explosionSounds.Length - 1));
        MakeSound(explosionSounds[i], volume);
    }

    public void MakeUnderwaterExplosionSound(float volume)
    {
        var i = Random.Range(0, (underwaterExplosionSounds.Length - 1));
        MakeSound(underwaterExplosionSounds[i], volume);
    }

    public void MakePlayerShotSound()
    {
        var i = Random.Range(0, (playerShotSounds.Length - 1));
        MakeSound(playerShotSounds[i], playerShotVolume);
    }

    public void MakeHitHurtSound()
    {
        var i = Random.Range(0, (hitHurtSounds.Length - 1));
        MakeSound(hitHurtSounds[i], hitHurtVolume);
    }

    public void MakePassedInSound(AudioClip sound)
    {
        MakeSound(sound);
    }


    private void MakeSound(AudioClip originalClip, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position, volume);
    }
}
