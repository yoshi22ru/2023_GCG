using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using Sources.InGame.BattleObject.Skill;
using UnityEngine;
using UnityEngine.Serialization;


namespace Sources.InGame.BattleObject.Castle
{
    public class Castle : BattleObject
    {
        [SerializeField] private int maxHP = 300;
        [SerializeField] private CastleDoor[] doors = new CastleDoor[2];
        //[SerializeField] private ParticleSystem particle;

        public HitPoint HitPoint
        {
            get;
            private set;
        }

        private void Awake()
        {
            HitPoint = new HitPoint(maxHP);
            HitPoint.IsDead.Where(x => x)
                .Subscribe(onNext: _ =>
                {
                    Destroy(gameObject);
                });
        }

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
                if (skillManager.type == SkillManager.SkillType.Damage)
                {
                    HitPoint.Damage(skillManager.GetSkillDamage);
                }
            }
        }

        protected override void OnHitMyTeamObject(BattleObject battleObject)
        {

        }
    }
}