using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharaDataBase", menuName = "CreateCharaDataBase")]
public class CharaDataBase : ScriptableObject
{
    public List<CharaData> charadata = new List<CharaData>();
}
