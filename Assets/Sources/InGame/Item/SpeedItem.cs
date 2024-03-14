using System.Collections;
using System.Collections.Generic;
using Sources.InGame.BattleObject.Character;
using UnityEngine;

public class SpeedItem : Item
{
    [SerializeField]
    private int speed = 7;
    private Renderer rend;
    private Collider coll;
    private CharacterStatus myCharacterStatus;
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
        if (timer > 30.0f)
        {
            AudioManager.Instance.PlaySE(AudioType.ItemLimit);
            myCharacterStatus.SetMoveSpeed(myCharacterStatus.MoveSpeed - speed);
            onTimer = false;
            Destroy(gameObject);
        }
    }

    public override void ItemEffect(CharacterStatus characterStatus)
    {
        rend.enabled = false;
        coll.enabled = false;
        AudioManager.Instance.PlaySE(AudioType.buffItem);
        myCharacterStatus = characterStatus;
        characterStatus.gameObject.GetComponent<CharacterStatus>();
        characterStatus.SetMoveSpeed(characterStatus.MoveSpeed + speed);

        onTimer = true;
        
    }
}
