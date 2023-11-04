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
    public virtual void OnHitMyTeamObject(BattleObject gameObject)
    {

    }
    [PunRPC]
    public virtual void OnHitEnemyTeamObject(BattleObject gameObject)
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
        if (!photonView.IsMine) return;
        
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
        else
        {
            Debug.Log("There is no Component of Battle Object");
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
