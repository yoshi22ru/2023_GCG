using System;
using System.Numerics;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UIElements;

public static class PlayerPropertiesExtensions
{
    #region HashKeys
    private const string CharacterKey = "Chara";
    private const string TeamKey = "Team";
    private const string DecisionKey = "Decision";
    private const string ScoreKey = "Score";
    private const string MessageKey = "Message";
    private const string HPKey = "hp";
    private const string TransformKey = "Transform";
    #endregion
    private static readonly Hashtable propsToSet = new Hashtable();

    #region Getter
    public static bool TryGetCharacter(this Player player, out CharaData.IdentCharacter character)
    {
        var ident = (player.CustomProperties[CharacterKey] is int chara) ? chara : -1;
        if (!Enum.IsDefined(typeof(CharaData.IdentCharacter), ident))
        {
            character = CharaData.IdentCharacter.Bird;
            return false;
        }

        character = (CharaData.IdentCharacter)ident;
        return true;
    }
    public static bool TryGetDecision(this Player player, out bool is_decide) {
        is_decide = (player.CustomProperties[DecisionKey] is bool decide) ? decide : false;
        return true;
    }
    public static bool TryGetTeam(this Player player, out int team_color) {
        team_color = (player.CustomProperties[TeamKey] is int team) ? team : -1;
        if (team_color == -1) {
            return false;
        }
        return true;
    }
    public static bool TryGetScore(this Player player, out int score) {
        score = (player.CustomProperties[ScoreKey] is int getscore) ? getscore : -1;
        if (score == -1) {
            return false;
        }
        return true;
    }

    public static bool TryGetMessage(this Player player, out string message) {
        message = (player.CustomProperties[MessageKey] is string getmessage) ? getmessage : string.Empty;
        if (message == string.Empty) {
            return false;
        }
        return true;
    }

    public static bool TryGetHp(this Player player, out int hp) {
        hp = (player.CustomProperties[HPKey] is short gethp) ? gethp : -1;
        if (hp == -1) {
            return false;
        }
        return true;
    }

    public static bool TryGetTransform(this Player player, out long trans) {
        trans = (player.CustomProperties[TransformKey] is long gettrans) ? gettrans : -1;
        if (trans == -1) {
            return false;
        }
        return true;
    }

    #endregion

    #region Setter
    public static void SetCharacter(this Player player, CharaData.IdentCharacter character)
    {
        propsToSet[CharacterKey] = character;
    }
    public static void SetDecision(this Player player, bool decide) {
        propsToSet[DecisionKey] = decide;
    }
    public static void SetTeam(this Player player, int team) {
        propsToSet[TeamKey] = team;
    }
    public static void SetScore(this Player player, int score) {
        propsToSet[ScoreKey] = score;
    }

    public static void SetMessage(this Player player, string message) {
        propsToSet[MessageKey] = message;
    }

    public static void SetHp(this Player player, int hp) {
        propsToSet[HPKey] = (short)hp;
    }

    public static void SetTransform(this Player player, Transform transform) {
        long trans = Protcol.TransformSerialize(transform.position, transform.rotation);
        propsToSet[TransformKey] = trans;
    }

    #endregion

    public static void SendPlayerProperties(this Player player) {
        if (propsToSet.Count > 0) {
            player.SetCustomProperties(propsToSet);
            propsToSet.Clear();
        }
    }
}