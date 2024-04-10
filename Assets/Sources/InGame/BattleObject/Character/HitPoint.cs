using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class HitPoint : IDisposable
{
    private readonly ReactiveProperty<int> _hp;
    private readonly int _maxHp;

    public ReadOnlyReactiveProperty<int> Hp => _hp;
    public readonly ReadOnlyReactiveProperty<bool> IsDead;

    public HitPoint(int maxHp)
    {
        _maxHp = maxHp;
        _hp = new ReactiveProperty<int>(maxHp);
        IsDead = Hp.Select(x => x <= 0).ToReadOnlyReactiveProperty();
    }

    public void Damage(int value)
    {
        if (value <= 0)
        {
            Debug.LogError("Invalid value");
            return;
        }

        _hp.Value -= value;
        Debug.Log("Damaged\n" +
                  $"/t after hp : {_hp.Value}");
    }

    public void Heal(int value)
    {
        if (value <= 0) 
        {
            Debug.LogError("Invalid value");
            return;
        }
        _hp.Value += value;
    }

    public void Revival()
    {
        _hp.Value = _maxHp;
    }

    public void Dispose()
    {
        _hp.Dispose();
        Hp.Dispose();
        IsDead.Dispose();
    }
}
