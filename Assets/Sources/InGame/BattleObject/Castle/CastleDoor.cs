using System.Collections;
using System.Collections.Generic;
using R3;
using Sources.InGame.BattleObject.Skill;
using UnityEngine;

namespace Sources.InGame.BattleObject.Castle
{
    public class CastleDoor : BattleObject
    {
        [SerializeField] private int maxHP = 100;
        [SerializeField] private ParticleSystem particle;

        private Renderer render;
        private float timer = 0;
        private bool onTimer = false;

        public HitPoint HitPoint
        {
            get;
            private set;
        }

        private void Awake()
        {
            render = GetComponent<Renderer>();
            HitPoint = new HitPoint(maxHP);
            HitPoint.IsDead.Where(x => x)
                .Subscribe(_ => Destroy(gameObject));
        }

        private void Update()
        {
            if (onTimer)
                timer += Time.deltaTime;

            if (timer > 0.7)
            {
                render.material.color = Color.white;
                timer = 0;
                onTimer = false;
            }
        }

        protected override void OnHitEnemyTeamObject(BattleObject battleObject)
        {
            render.material.color = Color.red;
            onTimer = true;

            ParticleSystem newParticle = Instantiate(particle);
            newParticle.transform.position = this.transform.position;
            newParticle.Play();
            Destroy(newParticle.gameObject, 0.5f);

            SkillManager skillManager = battleObject as SkillManager;
            if (skillManager == null)
                return;
            if (skillManager.type == SkillManager.SkillType.Damage)
            {
                HitPoint.Damage(skillManager.GetSkillDamage);
            }
        }

        protected override void OnHitMyTeamObject(BattleObject battleObject)
        {

        }
    }
}