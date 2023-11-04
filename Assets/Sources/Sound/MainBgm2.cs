using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBgm2 : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayBGM(AudioType.MainBgm2);
    }
}
