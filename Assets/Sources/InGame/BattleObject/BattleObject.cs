using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
// using UnityEditor.Build.Content;
using UnityEngine;

namespace Sources.InGame.BattleObject
{

    public class BattleObject : MonoBehaviourPunCallbacks
    {
        #region variables

        [SerializeField] Team team;
        ObjectType _objectType;

        #endregion

        protected virtual void OnHitMyTeamObject(BattleObject battleObject)
        {
        }

        protected virtual void OnHitEnemyTeamObject(BattleObject battleObject)
        {
        }

        #region accessor

        public Team GetTeam()
        {
            return team;
        }

        public void SetTeam(Team team)
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


        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("BattleObject")) return;
            
            if (other.gameObject.TryGetComponent<BattleObject>(out var battleObject))
            {
                Debug.Log("Try Get Component is success\n" +
                          $"ThisName : {name}\n" +
                          $"ObjectName : {other.name}\n" +
                          $"OtherTeam : {battleObject.team}\n" +
                          $"MyTeam : {team}");
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
                Debug.Log("Try Get Component is failure\n" +
                          $"ObjectName : {other.name}");
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
}
