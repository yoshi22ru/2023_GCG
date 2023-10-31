using System.Numerics;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public static class PlayerPropertiesExtensions
{
    #region HashKeys
    private const string CharaKey = "Chara";
    private const string ScoreKey = "Score";
    private const string MessageKey = "Message";
    private const string HPKey = "hp";
    private const string TransformKey = "Transform";
    #endregion
    private static readonly Hashtable propsToSet = new Hashtable();

    #region SelectCharactor

    public static int GetCharacter(this Player player) {
        return (player.CustomProperties[CharaKey] is int character) ? character : 0;
    }

    public static void SetCharacter(this Player player, int character) {
        propsToSet[CharaKey] = character;
    }

    #endregion

    #region BattleScene
    public static int GetScore(this Player player) {
        return (player.CustomProperties[ScoreKey] is int score) ? score : 0;
    }

    public static string GetMessage(this Player player) {
        return (player.CustomProperties[MessageKey] is string message) ? message : string.Empty;
    }

    public static int GetHp(this Player player) {
        return (int) ((player.CustomProperties[HPKey] is short hp) ? hp : 0);
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
    #endregion

    public static void SendPlayerProperties(this Player player) {
        if (propsToSet.Count > 0) {
            player.SetCustomProperties(propsToSet);
            propsToSet.Clear();
        }
    }
}