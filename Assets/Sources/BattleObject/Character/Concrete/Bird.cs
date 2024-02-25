using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Sources.BattleObject.Character.Concrete
{

    public class Bird : Character
    {
        // Temporary implementation
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
            AudioSourceCache.PlayOneShot(Skill1SE);
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
            AudioSourceCache.PlayOneShot(Skill2SE);
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
            Instantiate(SpecialPrefab, Skill2Point.position, myTransform.rotation);
            AudioSourceCache.PlayOneShot(SpecialSE);
        }
    }
}