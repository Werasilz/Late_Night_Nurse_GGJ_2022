using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        // int numMusicPlayers = FindObjectsOfType<AudioSource>().Length;
        // if (numMusicPlayers != 1)
        // {
        //     Destroy(this.gameObject);
        // }
        // // if more then one music player is in the scene
        // //destroy ourselves
        // else
        // {
        //     DontDestroyOnLoad(gameObject);
        // }

        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying)
        {
            return;
        }
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
