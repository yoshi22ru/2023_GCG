using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBgm1 : MonoBehaviour
{
    private string sceneName;
    void Update()
    {
        AudioManager.Instance.PlayBGM(AudioType.MainBgm1);
        sceneName = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(gameObject);
        if (sceneName != "Title" && sceneName != "MenuScene") 
        {
            Destroy(gameObject);
        } 
    }
}
