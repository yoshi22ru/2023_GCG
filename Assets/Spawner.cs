using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject character;
    private float time;
    private CharacterStatus characterStatus;
    [SerializeField] private Transform spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindWithTag("Character");
        characterStatus = character.GetComponent<CharacterStatus>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (characterStatus.IsDead)
        {
            time += Time.deltaTime;
            if (time >= 2.0)
            {
                character.SetActive(false);
                character.transform.position = spawnPos.position;
                characterStatus.SetIsDead(false);
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
