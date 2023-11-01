using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catsle : BattleObject
{
    [SerializeField]
    private int HP = 300;
    [SerializeField]
    private int maxHP = 300;
    [SerializeField]
    private int minHP = 0;

    public bool broken = false;

    public override void OnHitEnemyTeamObject(BattleObject gameObject)
    {
        SkillManager skillManager = gameObject as SkillManager;
        if (skillManager == null)
            return;
        if (skillManager.type == SkillManager.SkillType.weekDamage)
        {
            int damage = skillManager.GetSkill1Damage;
            SetHP(HP - skillManager.GetSkill1Damage);
            if (HP <= 0)
                broken = true;
        }
        else if (skillManager.type == SkillManager.SkillType.midDamage)
        {
            int damage = skillManager.GetSkill2Damage;
            SetHP(HP - skillManager.GetSkill2Damage);
            if (HP <= 0)
                broken = true;
        }
        else if (skillManager.type == SkillManager.SkillType.strongDamage)
        {
            int damage = skillManager.GetSpecialDamage;
            SetHP(HP - skillManager.GetSpecialDamage);
            if (HP <= 0)
                broken = true;
        }
    }

    public override void OnHitMyTeamObject(BattleObject gameObject)
    {
        
    }

    private void SetHP(int hp)
    {
        if (hp >= maxHP)
            HP = maxHP;
        else if (hp <= 0)
            HP = 0;
        else if (hp > 0)
            HP = hp;
    }
}
