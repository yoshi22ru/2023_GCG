using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Sources.InGame.BattleObject;
using Unity.VisualScripting;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    public static readonly List<PlayerSelection> PlayerSelections = new List<PlayerSelection>();

    public struct PlayerSelection {
        public readonly BattleObject.Team Team;
        public CharaData.IdentCharacter Character;
        public readonly int ActorNumber;

        public PlayerSelection(BattleObject.Team team,CharaData.IdentCharacter identCharacter, int actorNumber) {
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

    public static CharaData.IdentCharacter GetCharacterByActorNum(int actorNumber)
    {
        return PlayerSelections.FirstOrDefault(raw => raw.ActorNumber == actorNumber).Character; 
    }

    public static BattleObject.Team GetTeamByActorNumber(int actorNumber)
    {
        return PlayerSelections.FirstOrDefault(raw => raw.ActorNumber == actorNumber).Team;
    }
}
