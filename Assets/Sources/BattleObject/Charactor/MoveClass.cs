using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MoveClass : MonoBehaviour
{
    public float moveSpeed; // 現在のスピード


    void Start()
    {
        // UniRX 移動処理を実施
        this.UpdateAsObservable()
            .Where(_ =>
                    (Input.GetAxis("Horizontal") != 0) ||
                    (Input.GetAxis("Vertical") != 0)
            )
            .Subscribe(_ => Move());
    }

    public void Move()
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
