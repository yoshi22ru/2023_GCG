using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using TMPro;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countDownText;
    public GameObject startLabel;
    [SerializeField] int countMin;
    [SerializeField] int countMax;
    public int count;
    public bool isCountFinish = false;
    public static CountDown instance;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            instance = this;
        }
        StartCoroutine(TimeCount());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isCountFinish)
        {
            count = (countMax - (int)Time.time);
            countDownText.text = count.ToString("D1");
            if (count <= countMin)
            {
                count = countMin;
                isCountFinish = true;
                countDownText.text = "";
            }
        }
    }

    private IEnumerator TimeCount()
    {
        yield return new WaitForSeconds(3f);
        startLabel.SetActive(true);
        yield return new WaitForSeconds(1f);
        startLabel.SetActive(false);
    }
}
