using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Character
{
    [CreateAssetMenu(fileName = nameof(CharaDataBase), menuName = "ScriptableObjects/CreateCharaDataBase")]
    public class CharaDataBase : ScriptableObject
    {
        public List<CharaData> characterData = new List<CharaData>();

        public Sprite GetSprite(CharaData.IdentCharacter character)
        {
            return characterData.FirstOrDefault(x => x.CharaName == character)?.CharaSprite;
        }
        public Sprite GetSprite(int index)
        {
            if (!(index >= 0 && index <= Enum.GetValues(typeof(CharaData.IdentCharacter)).Cast<int>().Max()))
            {
                return null;
            }

            CharaData.IdentCharacter character = (CharaData.IdentCharacter)index;
            return characterData.FirstOrDefault(x => x.CharaName == character)?.CharaSprite;
        }
    }
}