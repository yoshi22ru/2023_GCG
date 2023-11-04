using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBgm1 : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayBGM(AudioType.StageBgm1);
    }
}
