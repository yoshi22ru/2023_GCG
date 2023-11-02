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
    private CatsleDoor[] doors = new CatsleDoor[2];
    //[SerializeField] private ParticleSystem particle;

    public bool broken = false;

    public override void OnHitEnemyTeamObject(BattleObject gameObject)
    {
        if (doors[0] == null || doors[1] == null)
        {
            AudioManager.Instance.PlaySE(AudioType.catsleAttacked);
            //ParticleSystem newParticle = Instantiate(particle);
            //newParticle.transform.position = this.transform.position;
            //newParticle.Play();
            //Destroy(newParticle.gameObject, 1.0f);

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
