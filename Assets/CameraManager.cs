using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public GameObject myCharacter;

    [SerializeField] private Vector3 offset;
    private Transform follow;
    private Transform characterShotPoint;
    private Transform characterCameraPoint;
    private CinemachineVirtualCamera cam;
    private Vector3 cameraPos;
    private Vector3 cameraVelocity;
    private Vector3 TargetPosition => follow.position + offset;
    CameraManager cameraManager;
    bool Initialized = false;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    public void Initialize()
    {
        Debug.Log("cameramanager started");
        follow = myCharacter.transform;
        cam = GetComponent<CinemachineVirtualCamera>();
        cameraManager = GetComponent<CameraManager>();

        characterShotPoint = myCharacter.transform.Find("ShotPoint");
        characterCameraPoint = myCharacter.transform.Find("CameraPoint");

        cam.Follow = characterCameraPoint.transform;
        cam.LookAt = characterShotPoint.transform;
        cameraPos = TargetPosition;

        Initialized = true;
    }

    private void FixedUpdate()
    {
        if (!Initialized) return;

        var targetPos = TargetPosition;
        cameraPos = Vector3.SmoothDamp(cameraPos, targetPos, ref cameraVelocity, 0.5f);
    }
}
