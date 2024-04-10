using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{

    [SerializeField] AudioClip SE;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            audioSource.PlayOneShot(SE);
            Invoke("ChangeScene", 1f);
        }
    }

    void ChangeScene()
    {
            SceneManager.LoadScene("MenuScene2");    //Åu3Åv
    }
}