using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform camTrans;
    public Transform pivot;
    public Transform Tank;
    public Transform mainTransform;

    public CameraConfig cameraConfig;
   
    public float delta; // не убирать
    public Transform targetLook;
        
    // актуальные параметры
    public float mouseX;
    public float mouseY;
    public float smoothX;
    public float smoothY;
    public float smoothXvelocity;
    public float smoothYvelocity;
    public float LookAngle;
    public float titleAngle;
    


    void Update()
    {
        Tick();
    }
   
    void Tick()
    {
        HandlePosition();
        HandleRotation();
        Vector3 targetPosition = Vector3.Lerp(mainTransform.position, Tank.position, 1);
        mainTransform.position = targetPosition;
        TargetLook();
    }
    void TargetLook()
    {
        Ray ray = new Ray(camTrans.position, camTrans.forward * 2000);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            targetLook.position = Vector3.Lerp(targetLook.position, hit.point, Time.deltaTime * 40);

        }
        else
        {
            targetLook.position = Vector3.Lerp(targetLook.position, targetLook.transform.forward*200, Time.deltaTime );
        }
    }

    void HandlePosition()
    {
        float targetX = cameraConfig.normalX;
        float targetY = cameraConfig.normalY;
        float targetZ = cameraConfig.normalZ;

        //перемещение камеры

        Vector3 NewPivotPosition = pivot.localPosition;
        NewPivotPosition.x = targetX;
        NewPivotPosition.y = targetY;

        Vector3 NewCameraPosition = camTrans.localPosition;
        NewCameraPosition.z = targetZ;

        float t = delta * cameraConfig.pivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, NewPivotPosition, t);
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, NewCameraPosition, t);

    }
    void HandleRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (cameraConfig.turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXvelocity, cameraConfig.turnSmooth);// сглаживание
            smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYvelocity, cameraConfig.turnSmooth);// сглаживание
        }
        else
        {
            smoothX = mouseX;
            smoothY = mouseY;

        }
        LookAngle += smoothX * cameraConfig.Y_Rot_Speed;
        Quaternion targetRot = Quaternion.Euler(0, LookAngle, 0);
        mainTransform.rotation = targetRot;

        titleAngle -= smoothY * cameraConfig.Y_Rot_Speed;
        titleAngle = Mathf.Clamp(titleAngle, cameraConfig.minAngle, cameraConfig.maxAngle);
        pivot.localRotation = Quaternion.Euler(titleAngle, 0, 0);
    }


}
