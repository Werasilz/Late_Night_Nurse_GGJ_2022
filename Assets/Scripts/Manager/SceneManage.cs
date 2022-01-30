using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadSceneStart()
    {
        SoundEffectManager.Instance.PlayAudio(SoundEffectManager.Instance.AudioClips[0].buttonClick, this.transform.position, 0.5f);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        //GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
    }

    public void LoadScenePlay()
    {
        SoundEffectManager.Instance.PlayAudio(SoundEffectManager.Instance.AudioClips[0].buttonClick, this.transform.position, 0.5f);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadSceneExit()
    {
        SoundEffectManager.Instance.PlayAudio(SoundEffectManager.Instance.AudioClips[0].buttonClick, this.transform.position, 0.5f);
        Application.Quit();
    }
}
