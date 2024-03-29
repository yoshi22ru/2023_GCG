using System.Collections;
using System.Collections.Generic;
using Sources.InGame.BattleObject.Skill;
using UnityEngine;

namespace Sources.InGame.BattleObject.Castle
{
    public class CastleDoor : BattleObject
    {
        [SerializeField] private int HP = 100;
        [SerializeField] private int maxHP = 100;
        [SerializeField] private int minHP = 0;
        [SerializeField] private ParticleSystem particle;

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

            if (timer > 0.7)
            {
                render.material.color = Color.white;
                timer = 0;
                onTimer = false;
            }

            if (HP <= 0)
                Destroy(gameObject);
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
                SetHP(HP - skillManager.GetSkillDamage);
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
}