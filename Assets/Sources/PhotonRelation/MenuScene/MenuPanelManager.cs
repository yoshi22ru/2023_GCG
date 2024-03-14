using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


namespace Sources.PhotonRelation.MenuScene
{
    public class MenuPanelManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private MenuPanelDB panelDB;
        private PanelInfo _nowPanel;
        private Stack<PanelInfo> _panelInfos;

        private void Start()
        {
            _panelInfos = new Stack<PanelInfo>();
            SetPanel(MenuPanelDB.IdentPanel.SelectOnlineOrOffline);
        }

        public void ReturnPanel()
        {
            if (_panelInfos.Count == 0) return;
            
            var tmp = _panelInfos.Pop();
            _nowPanel.Dispose();
            _nowPanel = tmp;
        }

        public void ShiftPanel(MenuPanelDB.IdentPanel identPanel)
        {
            if (identPanel == _nowPanel.IdentPanel) return;
            
            _panelInfos.Push(_nowPanel);
            SetPanel(identPanel);
        }
        
        private void SetPanel(MenuPanelDB.IdentPanel identPanel)
        {
            var tmp = Instantiate(panelDB.GetPanel(identPanel), this.transform);
            _nowPanel = new PanelInfo(tmp, identPanel);
            if (tmp.TryGetComponent<MenuPanelBase>(out var panelBase))
            {
                panelBase.Manager = this;
            }
            else
            {
                throw new Exception("Panel not set script");
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected");
            Debug.Log(cause);
        }

        public void StartGame()
        {
            Debug.Log("Called Button");
            int stageNum = Random.Range(1, 2);
            photonView.RPC(nameof(RPCLoadScene), RpcTarget.All, stageNum);
        }

        [PunRPC]
        private void RPCLoadScene(int stageNum)
        {
            switch (stageNum)
            {
                case 1:
                    SceneManager.LoadScene("Stage1");
                    break;
                case 2:
                    SceneManager.LoadScene("Stage2");
                    break;
            }
        }

        private readonly struct PanelInfo
        {
            private readonly GameObject _gameObject;
            private readonly MenuPanelDB.IdentPanel _identPanel;

            public PanelInfo(GameObject gameObject, MenuPanelDB.IdentPanel identPanel)
            {
                _gameObject = gameObject;
                _identPanel = identPanel;
            }
            public GameObject GameObject
            {
                get => _gameObject;
            }

            public MenuPanelDB.IdentPanel IdentPanel
            {
                get => _identPanel;
            }

            public void Dispose()
            {
                Destroy(_gameObject);
            }
        }
    }
}