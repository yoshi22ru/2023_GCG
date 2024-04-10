using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //LoadScene‚ğg‚¤‚½‚ß‚É•K{I
using UnityEngine.UI;


public class NextScene : MonoBehaviour
{
    public int sceneindex;

    public void ChangeScenes()
    {
        Invoke("NextSceneStart", 0.5f);
    }

    public void NextSceneStart()
    {
        this.sceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(this.sceneindex - 1);
    }
}