using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Sources.BattleObject;
using Unity.VisualScripting;
using UnityEngine;

public class VariableManager : MonoBehaviourPunCallbacks
{
    public static BattleObject.Team my_team;
    public static CharaData.Ident_Character character;

    public static List<PlayerSelection> player_selections = new List<PlayerSelection>();

    public struct PlayerSelection {
        public BattleObject.Team team;
        public CharaData.Ident_Character _Character;
        public int actor_number;

        public PlayerSelection(BattleObject.Team team,CharaData.Ident_Character ident_Character, int actor_number) {
            this.team = team;
            this._Character = ident_Character;
            this.actor_number = actor_number;
        }
    }

    public static int GetIndex(BattleObject.Team team, int actor) {
        int res = 0;
        for (int i = 0; i < player_selections.Count; ++i) {
            if (player_selections[i].team == team) {
                if (player_selections[i].actor_number == actor) return res;

                ++res;
            }
        }
        return 0;
    }
}
