using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBgm3 : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayBGM(AudioType.MainBgm3);
    }   
}
