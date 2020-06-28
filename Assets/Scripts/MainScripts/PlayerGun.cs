using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour
{
    /// <summary>
    /// "PlayerGun" Добавляется на орудие
    /// </summary>

    public Transform cam;
    public Transform gunAim;
    public Texture2D textureAim;
    [Header("Скрипт основной камеры")]
    public CameraPosition playerCamera;

    
    [Header("Прицел")]
    public float sizeAim = 30f; // В процентах от исходной текстуры
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

    void OnGUI()
    {
        aimPosition = FindGunAimPosition();
        GUI.DrawTexture(aimPosition, textureAim);
    }

    Rect FindGunAimPosition()
    {
        Vector3 target = playerCamera.GetAimPoint();
        RaycastHit hit;
        Ray ray = new Ray(gunAim.transform.position, gunAim.transform.forward);

        float distToAim = Vector3.Distance(target, gunAim.transform.position);
        Vector3 currentGunTarget;
        if (Physics.Raycast(ray, out hit, 100))
        {
            currentGunTarget = hit.point;
        }
        else
        {
            currentGunTarget = gunAim.transform.TransformPoint(Vector3.forward * 100);
        }
        Debug.DrawLine(ray.origin, currentGunTarget, Color.blue);

        Vector3 screenPos = cam.GetComponent<Camera>().WorldToScreenPoint(currentGunTarget);
        screenPos.y = Screen.height - screenPos.y;

        // Из центра вычитаем половину ширины/высоты текстуры прицела
        float posAimX = screenPos.x - (textureAim.width * sizeAim / 100 / 2);
        float posAimY = screenPos.y - (textureAim.height * sizeAim / 100 / 2);
        float sizeAimX = textureAim.width * sizeAim / 100;
        float sizeAimY = textureAim.height * sizeAim / 100;

        return (new Rect(posAimX, posAimY, sizeAimX, sizeAimY));
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
        } else 
        {
            tempAngleGunX = Mathf.Clamp(tempAngleGunX, 0f, minimumAngleGun);
        }
        transform.localEulerAngles = new Vector3(tempAngleGunX, 0, 0);
        
    }
}
