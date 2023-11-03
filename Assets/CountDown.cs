using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class CountDown : MonoBehaviour
{
    [SerializeField] TextMeshPro countDownText;
    [SerializeField] int countMin;
    [SerializeField] int countMax;
    private int count;
    public bool isCountFinish = false;
    public static CountDown instance;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            instance = this;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isCountFinish)
        {
            count = (countMax - (int)Time.time);
            Debug.Log("スタートまで" + count);
            if (count <= countMin)
            {
                count = countMin;
                isCountFinish = true;
            }
        }
    }
}
