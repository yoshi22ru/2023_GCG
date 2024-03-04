using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    [SerializeField] private int healPoint = 30;

    public override void ItemEffect(CharacterStatus character)
    {
        if (healPoint + character.CurrentHP >= character.MaxHP)
            character.SetHP(character.MaxHP);
        else if (character.CurrentHP > 0)
            character.SetHP(character.CurrentHP + healPoint);
        AudioManager.Instance.PlaySE(AudioType.healItem);
        Destroy(gameObject);
    }
}