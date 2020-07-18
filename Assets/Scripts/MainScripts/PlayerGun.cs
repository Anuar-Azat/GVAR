using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour
{
    /// <summary>
    /// "PlayerGun" Добавляется на орудие
    /// </summary>

    public Transform cam;
    public Transform gunAim;
    [Header("Скрипт основной камеры")]
    public CameraPosition playerCamera;

    
    [Header("Прицел")]
    public float speedRotateGun = 0.3f;
    public float maximumAngleGun = 10f;
    public float minimumAngleGun = 5f;

    float maxDistanceToHitGun;

    Rect aimPosition;
    float tempAngleGunX;

    void FixedUpdate()
    {
        BarrelMove();
    }

    void BarrelMove()
    {
        Vector3 target = playerCamera.GetAimPoint();
        Quaternion directionGun = Quaternion.LookRotation(target - gunAim.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, directionGun, speedRotateGun);

        tempAngleGunX = transform.localEulerAngles.x;
        if (transform.localEulerAngles.x > 180f)
        {
            tempAngleGunX = Mathf.Clamp(tempAngleGunX, 360f - maximumAngleGun, 360f);
        }
        else
        {
            tempAngleGunX = Mathf.Clamp(tempAngleGunX, 0f, minimumAngleGun);
        }
        transform.localEulerAngles = new Vector3(tempAngleGunX, 0, 0);
    }

    public Vector3 GetGunAimOnScreen()
    {
        Vector3 target = playerCamera.GetAimPoint();
        RaycastHit hit;
        Ray ray = new Ray(gunAim.transform.position, gunAim.transform.forward);

        // float distToAim = Vector3.Distance(target, gunAim.transform.position); На случай, если нужно знать дистанцию до цели

        Vector3 currentGunTarget;
        int layer = 1 << LayerMask.NameToLayer("Default");
        if (Physics.Raycast(ray, out hit, 100, layer, QueryTriggerInteraction.Ignore))
        {
            currentGunTarget = hit.point;
        }
        else
        {
            // TODO: Улучшить наведение в одну точку
            currentGunTarget = gunAim.transform.TransformPoint(Vector3.forward * 100);
        }

        Vector3 screenPos = cam.GetComponent<Camera>().WorldToScreenPoint(currentGunTarget);

        return screenPos;
    }


}
