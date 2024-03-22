using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Sources.InGame.BattleObject.Skill;
using UnityEngine;

namespace Sources.InGame.BattleObject.Character.Concrete
{
    public class Bird : Character
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
            var instance = Instantiate(Skill1Prefab, Skill1Point.position, myTransform.rotation).GetComponent<BattleObject>();
            instance.SetTeam(GetTeam());
            // FIXME
            // AudioSourceCache.PlayOneShot(Skill1SE);
            AudioManager.Instance.PlaySE(AudioType.buffItem);
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
            var instance =Instantiate(Skill2Prefab, Skill2Point.position, myTransform.rotation).GetComponent<BattleObject>();
            instance.SetTeam(GetTeam());
            // FIXME
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
            var instance=Instantiate(SpecialPrefab, Skill2Point.position, myTransform.rotation).GetComponent<BattleObject>();
            instance.SetTeam(GetTeam());
            // FIXME
            // AudioSourceCache.PlayOneShot(SpecialSE);
        }
    }
}