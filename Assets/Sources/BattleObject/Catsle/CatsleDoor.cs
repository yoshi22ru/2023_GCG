using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsleDoor : BattleObject
{
    [SerializeField]
    private int HP = 100;
    [SerializeField]
    private int maxHP = 100;
    [SerializeField]
    private int minHP = 0;

    private Renderer render;
    private float timer = 0;
    private bool onTimer = false;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (onTimer)
            timer += Time.deltaTime;
    }

    public override void OnHitEnemyTeamObject(BattleObject gameObject)
    {
        render.material.color = Color.red;
        onTimer = true;
        if (timer > 0.3)
        {
            render.material.color = Color.white;
            timer = 0;
            onTimer = false;
        }

        SkillManager skillManager = gameObject as SkillManager;
        if (skillManager == null)
            return;
        if (skillManager.type == SkillManager.SkillType.weekDamage)
        {
            SetHP(HP - skillManager.GetSkill1Damage);
        }
        else if (skillManager.type == SkillManager.SkillType.midDamage)
        {
            SetHP(HP - skillManager.GetSkill2Damage);
        }
        else if (skillManager.type == SkillManager.SkillType.strongDamage)
        {
            SetHP(HP - skillManager.GetSpecialDamage);
        }

        if (HP <= 0)
            Destroy(gameObject);
    }

    public override void OnHitMyTeamObject(BattleObject gameObject)
    {

    }

    private void SetHP(int hp)
    {
        if (hp >= maxHP)
            HP = maxHP;
        else if (hp <= minHP)
            HP = 0;
        else if (hp > 0)
            HP = hp;
    }
}