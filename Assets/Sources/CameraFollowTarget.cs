using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    private GameObject player;  //�v���C���[���i�[�p
    private Vector3 offset; // ���΋����擾�p

    void Start()
    {
        //�@Player�̏����擾
        this.player = GameObject.Find("Colobus");

        // ���C���J�����i�������g�j��Player�Ƃ̑��΋��������߂�
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        //�@�V�����g�����X�t�H�[���̒l��������
        transform.position = player.transform.position + offset;
    }
}
