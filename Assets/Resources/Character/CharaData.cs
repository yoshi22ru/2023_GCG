using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CharaData), menuName = "ScriptableObjects/CreateCharaData")]
public class CharaData : ScriptableObject
{
    [SerializeField]
    private string chara_name;
    [SerializeField]
    private Sprite chara_image;
    [SerializeField]
    private GameObject charactor_prefab;

    public string CharaName {get => chara_name;}
    public Sprite CharaSprite {get => chara_image;}
    public GameObject CharactorPrefab {get => charactor_prefab;}

    public enum Ident_Character {
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
