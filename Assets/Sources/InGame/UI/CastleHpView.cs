using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using Sources.InGame.BattleObject.Castle;
using UnityEngine;
using UnityEngine.UI;

public class CastleHpView : MonoBehaviour
{
    [SerializeField] private Castle redCastle;
    [SerializeField] private Castle blueCastle;

    [SerializeField] private Image redHp;
    [SerializeField] private Image blueHp;

    private void Start()
    {
        redCastle.HitPoint.Hp.Subscribe(onNext: x =>
        {
            Debug.Log("Red Castle Damaged");
            redHp.fillAmount = redCastle.HitPoint.FillAmount();
        });
        blueCastle.HitPoint.Hp.Subscribe(onNext: x =>
        {
            Debug.Log("Blue Castle Damaged");
            blueHp.fillAmount = blueCastle.HitPoint.FillAmount();
        });
    }
}
