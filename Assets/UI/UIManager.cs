//using System.Collections;
//using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//でいけます。その場合は、クラス名「Test」を自分のクラス名に書き換えてください。
public class UIManager : MonoBehaviour
{
    //前項で作った円形スライダーを、インスペクターから紐付けしておく。
    [SerializeField]
    Image countDownImage;
    //前項で作ったカウントダウン用のTextを、インスペクターから紐付けしておく。
    [SerializeField]
    Text countDownText;
    [SerializeField]
    Image BackGroundImage;
    //我ながら微妙な命名。
    int countDownCount;
    //経過時間。
    float countDownElapsedTime;
    //カウントダウンの長さ。この場合は3秒。
    float countDownDuration = 3.0f;
    //カウントダウンを開始する時これを呼ぶ。
    public void StartCountDown()
    {
        StartCoroutine("CountDown");
    }
    IEnumerator CountDown()
    {
        countDownCount = 0;
        countDownElapsedTime = 0;
        //テキストの更新。
        countDownText.text = System.String.Format("{0}", Mathf.FloorToInt(countDownDuration));
        //多分、負荷的にはGameObjectへの参照は別に保持していた方が宜しいかと思うが割愛。
        countDownImage.gameObject.SetActive(true);
        countDownText.gameObject.SetActive(true);
        BackGroundImage.gameObject.SetActive(true);

        while (true)
        {
            countDownElapsedTime += Time.deltaTime;
            //円形スライダーの更新。fillAmountは0～1.0fの間で指定する。経過時間の小数点以下の値を入れている。
            countDownImage.fillAmount = countDownElapsedTime % 1.0f;
            if (countDownCount < Mathf.FloorToInt(countDownElapsedTime))
            {
                //1秒刻みでカウント。
                countDownCount++;
                //テキストの更新。
                countDownText.text = System.String.Format("{0}", Mathf.FloorToInt(countDownDuration - countDownCount));
            }
            if (countDownDuration <= countDownElapsedTime)
            {
                //カウントダウン終了。
                countDownImage.gameObject.SetActive(false);
                countDownText.gameObject.SetActive(false);
                BackGroundImage.gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }
}