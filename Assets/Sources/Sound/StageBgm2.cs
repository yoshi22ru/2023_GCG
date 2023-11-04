using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBgm2 : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayBGM(AudioType.StageBgm2);
    }
}
