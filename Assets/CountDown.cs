using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDownText;
    [SerializeField] GameObject startLabel;
    [SerializeField] int countMin;
    [SerializeField] int countMax = 3;
    private float _count;
    private bool _countStarted = false;

    private void FixedUpdate()
    {
        if (!_countStarted) return;

        countDownText.text = ((int)Math.Ceiling(_count)).ToString("D1");
        _count -= Time.deltaTime;

        if (_count <= 0)
        {
            _countStarted = false;
        }
    }

    public async void CountDownToStart(float waitTime)
    {
        _countStarted = true;
        _count = countMax;
        await UniTask.WaitUntil(() => !_countStarted);
        countDownText.text = "";
        startLabel.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        startLabel.SetActive(false);
    }
}
