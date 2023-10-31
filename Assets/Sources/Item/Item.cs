using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private bool getCollision = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!getCollision)
        {
            getCollision = true;
            Character character = collision.gameObject.GetComponent<Character>();
            ItemEffect(character);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        getCollision = false;
    }

    public abstract void ItemEffect(Character character);
}
