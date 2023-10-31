using System.Numerics;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UIElements;

public static class PlayerPropertiesExtensions
{
    #region HashKeys
    private const string ScoreKey = "Score";
    private const string MessageKey = "Message";
    private const string HPKey = "hp";
    private const string TransformKey = "Transform";
    #endregion
    private static readonly Hashtable propsToSet = new Hashtable();

    public static int GetCharacter(this Player player) {
        return (player.CustomProperties[ScoreKey] is int chara) ? chara : 0;
    }
    public static int GetScore(this Player player) {
        return (player.CustomProperties[ScoreKey] is int score) ? score : 0;
    }

    public static string GetMessage(this Player player) {
        return (player.CustomProperties[MessageKey] is string message) ? message : string.Empty;
    }

    public static int GetHp(this Player player) {
        return (player.CustomProperties[HPKey] is short hp) ? hp : 0;
    }

    public static long GetTransform(this Player player) {
        return (player.CustomProperties[TransformKey] is long trans) ? trans : 0;
    }

    public static void SetCharacter(this Player player, int chara) {
        propsToSet[ScoreKey] = chara;
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

    public static void SendPlayerProperties(this Player player) {
        if (propsToSet.Count > 0) {
            player.SetCustomProperties(propsToSet);
            propsToSet.Clear();
        }
    }
}
