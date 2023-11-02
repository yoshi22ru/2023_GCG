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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(SE);
            Invoke("ChangeScene", 2f);
        }
    }

    void ChangeScene()
    {
            SceneManager.LoadScene("MenuScene");    //Åu3Åv
    }
}