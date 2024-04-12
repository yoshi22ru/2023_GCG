using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Sources.PhotonRelation.MenuScene
{
    public class MenuPanelBase : MonoBehaviourPunCallbacks
    {
        private MenuPanelManager _manager;

        public MenuPanelManager Manager
        {
            protected get => _manager;
            set => _manager = value;
        }
    }
}