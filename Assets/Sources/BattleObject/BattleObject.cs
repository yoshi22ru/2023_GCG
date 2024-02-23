using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
// using UnityEditor.Build.Content;
using UnityEngine;

public class BattleObject : MonoBehaviourPunCallbacks
{
    #region variables
    [SerializeField]
    Team team;
    ObjectType objectType;
    protected GameManager manager;
    #endregion
    [PunRPC]
    protected virtual void OnHitMyTeamObject(BattleObject battleObject)
    {

    }
    [PunRPC]
    protected virtual void OnHitEnemyTeamObject(BattleObject battleObject)
    {

    }
    #region accessor

    public Team GetTeam()
    {
        return team;
    }
    protected void SetTeam(Team team)
    {
        this.team = team;
    }
    /*
    public int getHP() {
        return this.HP;
    }
    protected void SetHP(int HP) {
        if (HP < 0) {
            Debug.Log("Invarid Integer reseived");
        }
        this.HP = HP;
    }
    */
    public void setManager(GameManager gameManager)
    {
        this.manager = gameManager;
    }


    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<BattleObject>(out var battleObject))
        {
            if (this.team == battleObject.team)
            {
                OnHitMyTeamObject(battleObject);
            }
            else
            {
                OnHitEnemyTeamObject(battleObject);
            }
        }
    }

    public enum Team
    {
        Red,
        Blue,
    }
    public enum ObjectType
    {
        PlayerCharacter,
        NPC,
        DamagingObject,
    }
}
