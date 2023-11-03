using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Photon.Pun;

public class Mouse : Character
{
    [SerializeField] GameObject skill1;
    [SerializeField] GameObject skill2;
    [SerializeField] GameObject special;
    [SerializeField] Transform skill1Point;
    [SerializeField] Transform skill2Point;
    [SerializeField] Transform specialPoint;
    [SerializeField] AudioClip skill1SE;
    [SerializeField] AudioClip skill2SE;
    [SerializeField] AudioClip specialSE;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [PunRPC]
    protected override void Skill1()
    {
        Instantiate(skill1, skill1Point.position, transform.rotation);
        base.Skill1();
        audioSource.PlayOneShot(skill1SE);
    }

    [PunRPC]
    protected override void Skill2()
    {
        GameObject Obj = (Instantiate(skill2, specialPoint.position, transform.rotation));
        Obj.transform.parent = transform;
        base.Special();
        audioSource.PlayOneShot(skill2SE);
    }

    [PunRPC]
    protected override void Special()
    {
        GameObject Obj = (Instantiate(special, specialPoint.position, transform.rotation));
        Obj.transform.parent = transform;
        base.Special();
        audioSource.PlayOneShot(specialSE);
    }
    private void FixedUpdate()
    {
        if (photonView.IsMine) {
            p1 = p2;
            p2 = my_transform.position;
            elapsed_time = (half) Time.deltaTime;
        } else {
            elapsed_time += (half) Time.deltaTime;
            if (elapsed_time < InterpolationPeriod) {
                my_transform.position = HermiteSpline.Interpolate(p1, p2, v1, v2, elapsed_time/InterpolationPeriod);
            } else {
                my_transform.position = Vector3.LerpUnclamped(p1, p2, elapsed_time/InterpolationPeriod);
            }
        }

        if (characterStatus.IsDead)
        {
            time += Time.deltaTime;

            if (currentState != Character_State.Dead) {
                photonView.RPC(nameof(SetState), RpcTarget.All, Character_State.Dead);
                // SetState(Character_State.Dead);
            }
            characterStatus.SetHP(characterStatus.MaxHP);
        }

        if (characterStatus.IsDead == false && CountDown.instance.isCountFinish == true)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (currentState != Character_State.Run) {
                    photonView.RPC(nameof(SetState), RpcTarget.All, Character_State.Run);
                    // SetState(Character_State.Run);
                }
            }
            else if (Input.GetKey(KeyCode.E))
            {
                if (characterStatus.UseSkill1())
                {
                    photonView.RPC(nameof(Skill1), RpcTarget.All);
                    // Skill1();
                }
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                if (characterStatus.UseSkill2())
                {
                    photonView.RPC(nameof(Skill2), RpcTarget.All);
                    // Skill2();
                }
            }
            else if (Input.GetKey(KeyCode.X))
            {
                if (characterStatus.UseSpecial())
                {
                    photonView.RPC(nameof(Special), RpcTarget.All);
                    // Special();
                }
            }
            else if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.K))
            {
                characterStatus.SetIsDead(true);
            }
            else
            {
                if (currentState != Character_State.Idle) {
                    photonView.RPC(nameof(SetState), RpcTarget.All, Character_State.Idle);
                    // SetState(Character_State.Idle);
                }
            }
        }
    }
}
