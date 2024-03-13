using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Sources.InGame.BattleObject;
using Unity.VisualScripting;
using UnityEngine;

public class VariableManager : MonoBehaviourPunCallbacks
{
    public static readonly List<PlayerSelection> PlayerSelections = new List<PlayerSelection>();

    public struct PlayerSelection {
        public readonly BattleObject.Team Team;
        public CharaData.Ident_Character Character;
        public readonly int ActorNumber;

        public PlayerSelection(BattleObject.Team team,CharaData.Ident_Character identCharacter, int actorNumber) {
            this.Team = team;
            this.Character = identCharacter;
            this.ActorNumber = actorNumber;
        }
    }

    public static int GetIndex(BattleObject.Team team, int actor) {
        int res = 0;
        for (int i = 0; i < PlayerSelections.Count; ++i) {
            if (PlayerSelections[i].Team == team) {
                if (PlayerSelections[i].ActorNumber == actor) return res;

                ++res;
            }
        }
        return 0;
    }

    public static CharaData.Ident_Character GetCharacterByActorNum(int actorNumber)
    {
        return PlayerSelections.FirstOrDefault(raw => raw.ActorNumber == actorNumber).Character; 
    }

    public static BattleObject.Team GetTeamByActorNumber(int actorNumber)
    {
        return PlayerSelections.FirstOrDefault(raw => raw.ActorNumber == actorNumber).Team;
    }
}
