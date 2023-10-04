using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class BattleObject : MonoBehaviour
{
    #region variables

    Team team;
    ObjectType objectType;
    int HP;
    GameManager manager;
    #endregion
    
    void OnHitMyTeamObject(BattleObject gameObject) {

    }
    void OnHitEnemyTeamObject(BattleObject gameObject) {

    }
    #region accessor

    public Team GetTeam() {
        return team;
    }
    protected void SetTeam(Team team) {
        this.team = team;
    }
    public int getHP() {
        return this.HP;
    }
    protected void SetHP(int HP) {
        if (HP < 0) {
            Debug.Log("Invarid Integer reseived");
        }
        this.HP = HP;
    }
    public void setManager(GameManager gameManager) {
        this.manager = gameManager;
    }

    #endregion

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("BattleObject")) {
            BattleObject battleObject = other.gameObject.GetComponent<BattleObject>();

            if (this.team == battleObject.team) {
                OnHitMyTeamObject(battleObject);
            } else {
                OnHitEnemyTeamObject(battleObject);
            }
        }
    }
    public enum Team {
        Red,
        Blue,
    }
    public enum ObjectType {
        PlayerCharactor,
        NPC,
        DamagingObject,
    }
}
