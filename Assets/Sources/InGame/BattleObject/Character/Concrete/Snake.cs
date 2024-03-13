using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Sources.InGame.BattleObject.Character.Concrete
{
    public class Snake : Character
    {
        protected override void Skill1()
        {
            CharacterStatus.UseSkill1();
            SetState(CharacterState.Skill1);
            photonView.RPC(nameof(Skill1Sync), RpcTarget.All);
        }

        [PunRPC]
        private void Skill1Sync()
        {
            var instance = Instantiate(Skill1Prefab, Skill1Point.position, myTransform.rotation).GetComponent<BattleObject>();
            instance.SetTeam(GetTeam());
            // AudioSourceCache.PlayOneShot(Skill1SE);
        }

        protected override void Skill2()
        {
            CharacterStatus.UseSkill2();
            SetState(CharacterState.Skill2);
            photonView.RPC(nameof(Skill2Sync), RpcTarget.All);
        }

        [PunRPC]
        private void Skill2Sync()
        {
            var instance = Instantiate(Skill2Prefab, Skill2Point.position, myTransform.rotation).GetComponent<BattleObject>();
            instance.SetTeam(GetTeam());
            // AudioSourceCache.PlayOneShot(Skill2SE);
        }

        protected override void Special()
        {
            CharacterStatus.UseSpecial();
            SetState(CharacterState.Special);
            photonView.RPC(nameof(SpecialSync), RpcTarget.All);
        }

        [PunRPC]
        private void SpecialSync()
        {
            var obj = Instantiate(SpecialPrefab, Skill2Point.position, myTransform.rotation);
            obj.transform.parent = myTransform;
            obj.GetComponent<BattleObject>().SetTeam(GetTeam());
            // AudioSourceCache.PlayOneShot(SpecialSE);
        }
    }
}