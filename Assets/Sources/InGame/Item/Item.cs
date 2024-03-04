using System.Collections;
using System.Collections.Generic;
using Sources.InGame.BattleObject;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private bool getCollision = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BattleObject>(out var battleObject))
        {
            if (!getCollision)
            {
                getCollision = true;
                CharacterStatus character = collision.gameObject.GetComponent<CharacterStatus>();
                ItemEffect(character);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        getCollision = false;
    }

    public abstract void ItemEffect(CharacterStatus character);
}
