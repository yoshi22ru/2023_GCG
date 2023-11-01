using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Runtime.CompilerServices;

public class MoveClass : MonoBehaviour
{
    private CharacterStatus characterStatus;
    private float speed;
    void Start()
    {
        characterStatus = GetComponent<CharacterStatus>();
        speed = characterStatus.MoveSpeed;
        // UniRX ˆÚ“®ˆ—‚ðŽÀŽ{
        this.UpdateAsObservable()
            .Where(_ =>
                    (Input.GetAxis("Horizontal") != 0) ||
                    (Input.GetAxis("Vertical") != 0)
            )
            .Subscribe(_ => Move());
    }

    public void Move()
    {
        var x = Input.GetAxis("Vertical") * speed;
        var z = -Input.GetAxis("Horizontal") * speed;
        if (x != 0 || z != 0)
        {
            var direction = new Vector3(x, 0, z);
            transform.position += new Vector3(x * Time.deltaTime, 0, z * Time.deltaTime);
            transform.localRotation = Quaternion.LookRotation(direction);
        }
    }
}
