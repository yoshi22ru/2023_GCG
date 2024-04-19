using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class HitPoint : IDisposable
{
    public readonly ReactiveProperty<int> Hp;
    private readonly int _maxHp;

    public readonly ReadOnlyReactiveProperty<bool> IsDead;

    public HitPoint(int maxHp)
    {
        _maxHp = maxHp;
        Hp = new ReactiveProperty<int>(maxHp);
        IsDead = Hp.Select(x => x <= 0).ToReadOnlyReactiveProperty();
    }

    public void Damage(int value)
    {
        if (value <= 0)
        {
            Debug.LogError("Invalid value");
            return;
        }
        if (value > Hp.Value)
        {
            Hp.Value = 0;
            return;
        }
        Hp.Value -= value;
        Debug.Log("Damaged\n" +
                  $"\t after hp : {Hp.Value}");
    }

    public void Heal(int value)
    {
        if (value <= 0) 
        {
            Debug.LogError("Invalid value");
            return;
        }
        Hp.Value += value;
    }

    public void Revival()
    {
        Hp.Value = _maxHp;
    }

    public float FillAmount()
    {
        return (float)Hp.Value / _maxHp;
    }

    public void Dispose()
    {
        Hp.Dispose();
        Hp.Dispose();
        IsDead.Dispose();
    }
}
