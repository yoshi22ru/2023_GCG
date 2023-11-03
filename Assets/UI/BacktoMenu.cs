using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoMenu : MonoBehaviour
{
    public void changeSceneButton2()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
