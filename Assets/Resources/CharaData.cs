using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactorData", menuName = "CreateCharactorData")]
public class CharaData : ScriptableObject
{
    [SerializeField]
    private string chara_name;
    [SerializeField]
    private Sprite chara_image;
    [SerializeField]
    private int max_hp;
    [SerializeField]
    private GameObject charactor_prefab;

    public string CharaName {get => chara_name;}
    public Sprite CharaSprite {get => chara_image;}
    public int MaxHP {get => max_hp;}
    public GameObject CharactorPrefab {get => charactor_prefab;}
    
    public enum Ident_Charactor : int {
        saru,
        tori,
        inu,
        i,
    }
}
