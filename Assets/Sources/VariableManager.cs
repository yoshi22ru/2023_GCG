using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Sources.InGame.BattleObject;
using Sources.Sync;
using Unity.VisualScripting;
using UnityEngine;

public static class VariableManager
{
    public static RoomOption RoomOption;
    public static readonly List<PlayerSelection> PlayerSelections = new List<PlayerSelection>();

    public struct PlayerSelection {
        public readonly Team Team;
        public readonly CharaData.IdentCharacter Character;
        public readonly int ActorNumber;

        public PlayerSelection(Team team,CharaData.IdentCharacter identCharacter, int actorNumber) {
            this.Team = team;
            this.Character = identCharacter;
            this.ActorNumber = actorNumber;
        }
    }

    public static int GetIndex(Team team, int actor) {
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

    public static Team GetTeamByActorNumber(int actorNumber)
    {
        return PlayerSelections.FirstOrDefault(raw => raw.ActorNumber == actorNumber).Team;
    }
}
