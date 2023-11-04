using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBgm1 : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayBGM(AudioType.MainBgm1);
    }
}
