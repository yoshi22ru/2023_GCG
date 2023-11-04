using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeButton : MonoBehaviour
{
    public void DecideSe()
    {
        AudioManager.Instance.PlaySE(AudioType.DecideButton);
    }
    public void BackSe()
    {
        AudioManager.Instance.PlaySE(AudioType.BackButton);
    }
    public void ChangeSe()
    {
        AudioManager.Instance.PlaySE(AudioType.ChangeButton);
    }

}
