using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Sources.PhotonRelation.MenuScene;
using Unity.VisualScripting;


namespace Sources.PhotonRelation.SelectCharacterScene
{
    public class SelectCharacterPanel : MenuPanelBase
    {
        [SerializeField] private List<SelectCharacter> selections;

        private void Awake()
        {
            Debug.Log(PhotonNetwork.OfflineMode);
            for (int i = 0; i < selections.Count; ++i)
            {
                selections[i].SetActorNum(i + 1);
                selections[i].ChangeTeam();
            }
        }

        void FixedUpdate()
        {
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
                if (selections[i].GetTeam() == InGame.BattleObject.BattleObject.Team.Blue)
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

        void SetParams()
        {
            for (int i = 0; i < selections.Count; ++i)
            {
                VariableManager.player_selections.Add(new VariableManager.PlayerSelection(
                    selections[i].GetTeam(),
                    selections[i].GetCharacter(),
                    selections[i].GetActorNum()
                ));
            }
        }
    }
}