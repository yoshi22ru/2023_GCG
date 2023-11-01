using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item
{
    [SerializeField]
    private int speed = 10;
    private CharacterStatus characterStatus;
    private float timer = 0;
    private bool onTimer = false;

    private void Update()
    {
        if (onTimer)
            timer += Time.deltaTime;
    }

    public override void ItemEffect(CharacterStatus character)
    {
        characterStatus.gameObject.GetComponent<CharacterStatus>();
        characterStatus.SetMoveSpeed(characterStatus.MoveSpeed + speed);

        onTimer = true;
        if (timer > 5.0f)
        {
            characterStatus.SetMoveSpeed(characterStatus.MoveSpeed - speed);
            onTimer = false;
        }
    }
}
