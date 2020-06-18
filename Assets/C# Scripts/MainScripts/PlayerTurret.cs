using UnityEngine;
using System.Collections;

public class PlayerTurret : MonoBehaviour
{   
    
    public Transform cam;
    public Transform tank;
    [Header("Параметры башни")]
    public float speedRotateTurret = 10f;
    public float rangeAngleTurret = 45;
    Quaternion directionTurret;
    public float GunDamage;
    public PlayerCamera playerCamera;

    void FixedUpdate()
    {
        TurretMove();
    }

    void TurretMove()
    {
        Vector3 target = playerCamera.GetAimPoint();
        Quaternion directionGun = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, directionGun, speedRotateTurret * Time.deltaTime);
        float tempAngleTowerY = transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(0, tempAngleTowerY, 0);
    }
}
