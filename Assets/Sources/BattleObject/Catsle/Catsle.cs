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
    [SerializeField]
    private ParticleSystem particle;

    public bool broken = false;
    private Renderer render;
    private float timer = 0;
    private bool onTimer = false;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    private void Update()
    {
        if(onTimer)
            timer += Time.deltaTime;
    }

    public override void OnHitEnemyTeamObject(BattleObject gameObject)
    {
        render.material.color = Color.red;
        onTimer = true;
        if(timer > 0.3)
        {
            render.material.color = Color.white;
            timer = 0;
            onTimer = false;
        }
        ParticleSystem newParticle = Instantiate(particle);
        newParticle.transform.position = this.transform.position;
        newParticle.Play();
        Destroy(newParticle.gameObject, 0.5f);

        SkillManager skillManager = gameObject as SkillManager;
        if (skillManager == null)
            return;
        if (skillManager.type == SkillManager.SkillType.weekDamage)
        {
            SetHP(HP - skillManager.GetSkill1Damage);
            if (HP <= 0)
                broken = true;
        }
        else if (skillManager.type == SkillManager.SkillType.midDamage)
        {
            SetHP(HP - skillManager.GetSkill2Damage);
            if (HP <= 0)
                broken = true;
        }
        else if (skillManager.type == SkillManager.SkillType.strongDamage)
        {
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
        else if (hp <= minHP)
            HP = 0;
        else if (hp > 0)
            HP = hp;
    }
}
