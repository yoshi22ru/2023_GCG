using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // ���݂̃X�s�[�h

    private void Start()
    {
        // UniRX �ړ����������{
        this.UpdateAsObservable()
            .Where(_ =>
                    (Input.GetAxis("Horizontal") != 0) ||
                    (Input.GetAxis("Vertical") != 0)
            )
            .Subscribe(_ => Move());
    }

    void Move()
    {

        var x = Input.GetAxis("Vertical") * moveSpeed;
        var z = -Input.GetAxis("Horizontal") * moveSpeed;

        if (x != 0 || z != 0)
        {
            var direction = new Vector3(x, 0, z);
            transform.position += new Vector3(x * Time.deltaTime, 0, z * Time.deltaTime);
            transform.localRotation = Quaternion.LookRotation(direction);
        }

    }

}