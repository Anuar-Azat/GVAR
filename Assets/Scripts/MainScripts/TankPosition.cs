using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPosition : MonoBehaviour
{
    private Transform tank;
    public Transform TarCamRot;
    private Transform tarCam;
    [Header("Скорость следования за вращением башни вокруг оси танка")]
    public float speed = 0.13f;
    [Space]
    public float minAngle;
    public float maxAngle;
    public float speedCameraTarget;
    public float speedRotateCamera = 5f;
    public float speedScroll = 5f;
    [Space]
    public float maxHight = -28;
    public float minHight = -11;
    [Space]
    public float correction;

    [Header("Ограничение наклона")]
    public float minimumY = -20f;
    public float maximumY = 10f;
    private float rotationX;
    private float rotationY;
    
    private float scroll;


    void Start()
    {
        transform.parent = null;
        tarCam = transform.GetChild(0);
        tank = TarCamRot.transform.parent;
        transform.rotation = Quaternion.identity;
        tarCam.rotation = tank.rotation;
    }

    void FixedUpdate()
    {
        transform.position = tank.position;

        rotationX += Input.GetAxis("Mouse X") * speedRotateCamera;
        rotationY -= Input.GetAxis("Mouse Y") * speedRotateCamera;
        scroll += Input.GetAxis("Mouse ScrollWheel") * speedRotateCamera;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);
        //tarCam.transform.position = Vector3.Lerp(1,1,1);

    }
}
