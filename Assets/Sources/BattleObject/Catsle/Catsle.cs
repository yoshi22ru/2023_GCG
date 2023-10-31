using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catsle : BattleObject
{
    private Material material;

    private void Start()
    {
        material = GetComponent<Material>();
    }

    public override void OnHitMyTeamObject(BattleObject gameObject)
    {

    }

    public override void OnHitEnemyTeamObject(BattleObject gameObject)
    {
        material.color = Color.red;
        Invoke("ChangeWhite", 0.2f);
    }

    private void ChangeWhite()
    {
        material.color = Color.white;
    }
}
