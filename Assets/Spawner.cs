using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Sources.InGame.BattleObject.Character;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject character;
    private float time;
    private CharacterStatus characterStatus;
    private Vector3 pos;

    void Start()
    {
        character = GameObject.FindWithTag("Character");
        characterStatus = character.GetComponent<CharacterStatus>();
        pos = SpawnPosition.instance.spawnPos;
    }

    void FixedUpdate()
    {
        if (characterStatus.IsDead)
        {
            time += Time.deltaTime;
            if (time >= 2.0)
            {
                character.SetActive(false);
                character.transform.position = pos;
                time = 0;
            }
        }
        else
        {
            time += Time.deltaTime;
            if (time >= 10)
            {
                character.SetActive(true);
                time = 0;
            }
        }
    }

}
