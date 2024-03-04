using System.Collections;
using System.Collections.Generic;
using Sources.InGame.BattleObject.Skill;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    private GameObject[] items = new GameObject[3];
    [SerializeField]
    private ParticleSystem particle;
    private int[] itemProbability = new int[3];
    private Vector3 pos;
    private Renderer rend;
    private Collider coll;

    private void Start()
    {
        pos = transform.position;
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.TryGetComponent<SkillManager>(out var skill))
        {
            if (skill.type is SkillManager.SkillType.midDamage or SkillManager.SkillType.weekDamage or SkillManager.SkillType.strongDamage)
            {
                ParticleSystem newParticle = Instantiate(particle);
                newParticle.transform.position = this.transform.position;
                newParticle.Play();
                rend.enabled = false;
                coll.enabled = false;
                Destroy(newParticle.gameObject, 0.5f);
                Invoke(nameof(Gatya), 0.5f);
                Destroy(gameObject, 0.7f);
            }
        }
    }

    private void Gatya()
    {
        itemProbability[0] = 30;
        itemProbability[1] = 30;
        itemProbability[2] = 30;

        int result = Choose(itemProbability);
        Instantiate(items[result], new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
    }

    private int Choose(int[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }

        return probs.Length - 1;
    }
}
