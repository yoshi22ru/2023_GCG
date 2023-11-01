using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : Item
{
    [SerializeField]
    private int attack = 10;
    private SkillManager skillManager;
    private float timer = 0;
    private bool onTimer = false;

    private void Update()
    {
        if (onTimer)
            timer += Time.deltaTime;
    }

    public override void ItemEffect(CharacterStatus character)
    {
        GameObject[] skill = character.GetSkillPrefab;
        for (int i = 0; i < skill.Length; i++)
        {
            skillManager = skill[i].gameObject.GetComponent<SkillManager>();
            skillManager.SetSkill1Damage(skillManager.GetSkill1Damage + attack);
            skillManager.SetSkill1Damage(skillManager.GetSkill2Damage + attack);
            skillManager.SetSkill1Damage(skillManager.GetSpecialDamage + attack);
        }

        onTimer = true;
        if(timer > 5.0f)
        {
            for (int i = 0; i < skill.Length; i++)
            {
                skillManager = skill[i].gameObject.GetComponent<SkillManager>();
                skillManager.SetSkill1Damage(skillManager.GetSkill1Damage - attack);
                skillManager.SetSkill1Damage(skillManager.GetSkill2Damage - attack);
                skillManager.SetSkill1Damage(skillManager.GetSpecialDamage - attack);
            }
            onTimer = false;
        }
    }
}
