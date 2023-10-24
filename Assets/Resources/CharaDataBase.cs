using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharaDataBase", menuName = "CreateCharaDataBase")]
public class CharaDataBase : ScriptableObject
{
    public List<Character> charadata = new List<Character>();
}
