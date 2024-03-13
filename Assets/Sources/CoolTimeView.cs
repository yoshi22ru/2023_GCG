using System.Collections;
using System.Collections.Generic;
using Sources.InGame.BattleObject.Character;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeView : MonoBehaviour
{
    [SerializeField] Image Skill1;
    [SerializeField] Image Skill2;
    [SerializeField] Image Special;
    private CharacterStatus playerStatus;
    float skill1cool_len => playerStatus.GetSkill1Cool();
    float skill2cool_len => playerStatus.GetSkill2Cool();
    float specialcool_len => playerStatus.GetSpecialCool();

    float skill1cool_now => playerStatus.GetCurrentSkill1Cool();
    float skill2cool_now => playerStatus.GetCurrentSkill2Cool();
    float specialcool_now => playerStatus.GetCurrentSpecialCool();

    bool is_init = false;

    void FixedUpdate() {
        if (!is_init) return;

        Skill1.fillAmount = (skill1cool_len - skill1cool_now)/skill1cool_len;
        Skill2.fillAmount = (skill2cool_len - skill2cool_now)/skill2cool_len;
        Special.fillAmount = (specialcool_len - specialcool_now)/specialcool_len;
    }

    public void SetStatus(CharacterStatus playerStatus) {
        this.playerStatus = playerStatus;

        is_init = true;
    }
}
