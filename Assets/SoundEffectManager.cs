using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundEffectManager : MonoBehaviour
{
    public GameObject audioSpawner;

    public AudioClipContain[] AudioClips;

    public static SoundEffectManager Instance;

    [HideInInspector] public int bagIndexPlaying;
    [HideInInspector] public int ghostIndexPlaying;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayAudio(AudioClip audioClip, Vector3 position, float volume)
    {
        GameObject audioSpawn = Instantiate(audioSpawner, position, Quaternion.identity);
        audioSpawn.transform.SetParent(gameObject.transform);
        audioSpawn.GetComponent<AudioSource>().clip = audioClip;
        audioSpawn.GetComponent<AudioSource>().volume = volume;
        audioSpawn.GetComponent<AudioSource>().Play();
        Destroy(audioSpawn, 5f);
    }

    public void PlayBagSound(Transform target)
    {
        if (bagIndexPlaying >= AudioClips[0].bag.Length)
        {
            bagIndexPlaying = 0;
        }

        PlayAudio(AudioClips[0].bag[bagIndexPlaying], target.position, 0.5f);
        bagIndexPlaying += 1;
    }

    public void PlayGhostSound(Transform target)
    {
        if (ghostIndexPlaying >= AudioClips[0].ghost.Length)
        {
            ghostIndexPlaying = 0;
        }

        PlayAudio(AudioClips[0].ghost[ghostIndexPlaying], target.position, 0.5f);
        ghostIndexPlaying += 1;
    }
}

[System.Serializable]
public class AudioClipContain
{
    public AudioClip[] bag;
    public AudioClip[] ghost;
    public AudioClip exitDoor;
    public AudioClip corpseLost;
    public AudioClip buttonClick;
}
