using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Sources.InGame.BattleObject.Character.Concrete
{


    public class Mouse : Character
    {
        protected override void Skill1()
        {
            CharacterStatus.UseSkill1();
            SetStateAndResetIdle(CharacterState.Skill1);
            photonView.RPC(nameof(Skill1Sync), RpcTarget.All);
        }

        [PunRPC]
        private void Skill1Sync()
        {
            Instantiate(Skill1Prefab, Skill1Point.position, myTransform.rotation)
                .GetComponent<BattleObject>()
                .SetTeam(GetTeam());
            // AudioSourceCache.PlayOneShot(Skill1SE);
        }

        protected override void Skill2()
        {
            CharacterStatus.UseSkill2();
            SetStateAndResetIdle(CharacterState.Skill2);
            photonView.RPC(nameof(Skill2Sync), RpcTarget.All);
        }

        [PunRPC]
        private void Skill2Sync()
        {
            var obj = Instantiate(Skill2Prefab, Skill2Point.position, myTransform.rotation);
            obj.transform.parent = myTransform;
            obj.GetComponent<BattleObject>().SetTeam(GetTeam());

            // AudioSourceCache.PlayOneShot(Skill2SE);
        }

        protected override void Special()
        {
            CharacterStatus.UseSpecial();
            SetStateAndResetIdle(CharacterState.Special);
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