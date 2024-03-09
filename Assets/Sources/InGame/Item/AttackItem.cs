using System.Collections;
using System.Collections.Generic;
using Sources.InGame.BattleObject.Skill;
using UnityEngine;

public class AttackItem : Item
{
    [SerializeField]
    private int attack = 10;
    private SkillManager skillManager;
    private Renderer rend;
    private Collider coll;
    private GameObject[] skill;
    private float timer = 0;
    private bool onTimer = false;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider>();
    }

    private void Update()
    {
        if (onTimer)
            timer += Time.deltaTime;
        
        if(timer > 30.0f)
        {
            AudioManager.Instance.PlaySE(AudioType.ItemLimit);
            for (int i = 0; i < skill.Length; i++)
            {
                skillManager = skill[i].gameObject.GetComponent<SkillManager>();
                skillManager.SetSkillDamage(skillManager.GetSkillDamage - attack);
            }
            onTimer = false;
            Destroy(gameObject);
        }
    }

    public override void ItemEffect(CharacterStatus character)
    {
        rend.enabled = false;
        coll.enabled = false;
        AudioManager.Instance.PlaySE(AudioType.buffItem);
        skill = character.GetSkillPrefab;
        for (int i = 0; i < skill.Length; i++)
        {
            skillManager = skill[i].gameObject.GetComponent<SkillManager>();
            skillManager.SetSkillDamage(skillManager.GetSkillDamage + attack);
        }

        onTimer = true;
    }
}
