//using System.Collections;
//using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//�ł����܂��B���̏ꍇ�́A�N���X���uTest�v�������̃N���X���ɏ��������Ă��������B
public class UIManager : MonoBehaviour
{
    //�O���ō�����~�`�X���C�_�[���A�C���X�y�N�^�[����R�t�����Ă����B
    [SerializeField]
    Image countDownImage;
    //�O���ō�����J�E���g�_�E���p��Text���A�C���X�y�N�^�[����R�t�����Ă����B
    [SerializeField]
    Text countDownText;
    [SerializeField]
    Image BackGroundImage;
    //��Ȃ�������Ȗ����B
    int countDownCount;
    //�o�ߎ��ԁB
    float countDownElapsedTime;
    //�J�E���g�_�E���̒����B���̏ꍇ��3�b�B
    float countDownDuration = 3.0f;
    //�J�E���g�_�E�����J�n���鎞������ĂԁB
    public void StartCountDown()
    {
        StartCoroutine("CountDown");
    }
    IEnumerator CountDown()
    {
        countDownCount = 0;
        countDownElapsedTime = 0;
        //�e�L�X�g�̍X�V�B
        countDownText.text = System.String.Format("{0}", Mathf.FloorToInt(countDownDuration));
        //�����A���דI�ɂ�GameObject�ւ̎Q�Ƃ͕ʂɕێ����Ă��������X�������Ǝv���������B
        countDownImage.gameObject.SetActive(true);
        countDownText.gameObject.SetActive(true);
        BackGroundImage.gameObject.SetActive(true);

        while (true)
        {
            countDownElapsedTime += Time.deltaTime;
            //�~�`�X���C�_�[�̍X�V�BfillAmount��0�`1.0f�̊ԂŎw�肷��B�o�ߎ��Ԃ̏����_�ȉ��̒l�����Ă���B
            countDownImage.fillAmount = countDownElapsedTime % 1.0f;
            if (countDownCount < Mathf.FloorToInt(countDownElapsedTime))
            {
                //1�b���݂ŃJ�E���g�B
                countDownCount++;
                //�e�L�X�g�̍X�V�B
                countDownText.text = System.String.Format("{0}", Mathf.FloorToInt(countDownDuration - countDownCount));
            }
            if (countDownDuration <= countDownElapsedTime)
            {
                //�J�E���g�_�E���I���B
                countDownImage.gameObject.SetActive(false);
                countDownText.gameObject.SetActive(false);
                BackGroundImage.gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }
}