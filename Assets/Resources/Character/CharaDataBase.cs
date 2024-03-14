using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Character
{
    [CreateAssetMenu(fileName = nameof(CharaDataBase), menuName = "ScriptableObjects/CreateCharaDataBase")]
    public class CharaDataBase : ScriptableObject
    {
        public List<CharaData> characterData = new List<CharaData>();
    }
}