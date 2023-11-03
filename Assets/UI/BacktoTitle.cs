using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoTitle : MonoBehaviour
{
    public void changeSceneButton()
    {
        SceneManager.LoadScene("Title");
    }
}
