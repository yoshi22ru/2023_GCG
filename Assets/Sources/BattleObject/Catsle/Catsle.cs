using System.Collections;
using System.Collections.Generic;
using Sources.BattleObject;
using Sources.BattleObject.Character;
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

    protected override void OnHitEnemyTeamObject(BattleObject battleObject)
    {
        if (doors[0] == null || doors[1] == null)
        {
            AudioManager.Instance.PlaySE(AudioType.catsleAttacked);
            //ParticleSystem newParticle = Instantiate(particle);
            //newParticle.transform.position = this.transform.position;
            //newParticle.Play();
            //Destroy(newParticle.gameObject, 1.0f);

            SkillManager skillManager = battleObject as SkillManager;
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

    protected override void OnHitMyTeamObject(BattleObject battleObject)
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
