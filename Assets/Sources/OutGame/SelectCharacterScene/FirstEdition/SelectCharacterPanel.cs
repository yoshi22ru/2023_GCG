using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Sources.InGame.BattleObject;
using Sources.OutGame.MenuScene;


namespace Sources.OutGame.SelectCharacterScene
{
    public class SelectCharacterPanel : MenuPanelBase
    {
        [SerializeField] private List<SelectCharacter> selections;
        [SerializeField] private Text roomName;

        private void Awake()
        {
            roomName.text = PhotonNetwork.CurrentRoom.Name;
            for (int i = 0; i < selections.Count; ++i)
            {
                selections[i].SetActorNum(i + 1);
                selections[i].ChangeTeam();
            }
        }

        void FixedUpdate()
        {
            // FIXME : reactive call
            CheckAll();
        }

        void CheckAll()
        {
            int i = 0;
            for (; i < selections.Count; ++i)
            {
                if (!selections[i].GetDecision())
                {
                    break;
                }
            }

            if (i == selections.Count & IsEven())
            {
                SetParams();
                Manager.ShiftPanel(MenuPanelDB.IdentPanel.StartGame);
            }

            if (PhotonNetwork.OfflineMode && selections[0].GetDecision())
            {
                SetParams();
                Manager.ShiftPanel(MenuPanelDB.IdentPanel.StartGame);
            }
        }


        
        private bool IsEven()
        {
            int count = 0;

            for (int i = 0; i < selections.Count; ++i)
            {
                if (selections[i].GetTeam() == Team.Blue)
                {
                    ++count;
                }
            }

            if (count == selections.Count / 2)
            {
                return true;
            }

            return false;
        }

        private void SetParams()
        {
            var message="";
            foreach (var select in selections)
            {
                message += $"Number : {select.GetActorNum()}\n" +
                           $"Team   : {select.GetTeam()}\n" +
                           $"Chara  : {select.GetCharacter()}\n";
                VariableManager.PlayerSelections.Add(new VariableManager.PlayerSelection(
                    select.GetTeam(),
                    select.GetCharacter(),
                    select.GetActorNum()));
            }

            // Debug.Log(message);
        }
    }
}