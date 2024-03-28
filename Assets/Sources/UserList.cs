using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class UserList
{
    private readonly bool[] _ready;

    public UserList(int number)
    {
        Debug.Log($"List construct arg : {number}");
        _ready = new bool[number];
    }

    public void SetReady(int actorNumber)
    {
        // actor number start from 1
        actorNumber--;
        if (actorNumber < 0 || actorNumber >= _ready.Length)
        {
            throw new InvalidDataException($"arg : {actorNumber}\n" +
                                           $"length : {_ready.Length}");
        }
        _ready[actorNumber] = true;
    }

    public bool CheckReady()
    {
        return _ready.All(x => x);
    }
}
