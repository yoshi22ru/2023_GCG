using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Sources.InGame.BattleObject.Character.Concrete
{


    public class Gecho : Character
    {
        protected override void Skill1()
        {
            characterStatus.UseSkill1();
            SetState(Character_State.Skill1);
            photonView.RPC(nameof(Skill1Sync), RpcTarget.All);
        }

        [PunRPC]
        private void Skill1Sync()
        {
            Instantiate(Skill1Prefab, Skill1Point.position, myTransform.rotation);
            // AudioSourceCache.PlayOneShot(Skill1SE);
        }

        protected override void Skill2()
        {
            characterStatus.UseSkill2();
            SetState(Character_State.Skill2);
            photonView.RPC(nameof(Skill2Sync), RpcTarget.All);
        }

        [PunRPC]
        private void Skill2Sync()
        {
            Instantiate(Skill2Prefab, Skill2Point.position, myTransform.rotation);
            // AudioSourceCache.PlayOneShot(Skill2SE);
        }

        protected override void Special()
        {
            characterStatus.UseSpecial();
            SetState(Character_State.Special);
            photonView.RPC(nameof(SpecialSync), RpcTarget.All);
        }

        [PunRPC]
        private void SpecialSync()
        {
            var obj = Instantiate(SpecialPrefab, Skill2Point.position, myTransform.rotation);
            obj.transform.parent = myTransform;
            // AudioSourceCache.PlayOneShot(SpecialSE);
        }
    }
}