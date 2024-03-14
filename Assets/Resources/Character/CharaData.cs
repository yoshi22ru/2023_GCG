using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(CharaData), menuName = "ScriptableObjects/CreateCharaData")]
public class CharaData : ScriptableObject
{
    [SerializeField]
    private IdentCharacter charaName;
     [SerializeField]
    private Sprite charaImage;
    [SerializeField]
    private GameObject characterPrefab;

    public IdentCharacter CharaName {get => charaName;}
    public Sprite CharaSprite {get => charaImage;}
    public GameObject CharacterPrefab {get => characterPrefab;}

    public enum IdentCharacter {
        Bird,
        Deer,
        Fish,
        Gecko,
        Monkey,
        Mouse,
        Snake,
        Squid,
    }
}
