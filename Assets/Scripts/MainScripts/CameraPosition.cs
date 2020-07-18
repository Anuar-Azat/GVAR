using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour
{
    public Transform Rotate;
    public Transform Target;
    public float speedRotateCamera = 5f;
    public float cameraPositionZ = -10f;
    public float minDistanceZ = -20f;
    public float maxDistanceZ = 50f;
    public float speedScroll = 30f;



    void Start()
    {
        transform.parent = null;
    }

    void FixedUpdate()
    {
        cameraPositionZ = Mathf.Clamp(cameraPositionZ + Input.GetAxis("Mouse ScrollWheel") * speedScroll, minDistanceZ, maxDistanceZ);
        print(cameraPositionZ);
        Target.transform.localPosition += new Vector3 (0, 0, Input.GetAxis("Mouse ScrollWheel") * speedScroll);

        transform.position = Vector3.Lerp(transform.position, Target.position, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, Target.rotation, speedRotateCamera);
    }

    public Vector3 GetAimPoint()
    /// <summary>
    /// Функция поиска точки экранного прицела
    /// </summary>
    /// <returns>Vector3 точки в пространстве</returns>
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Vector3 currentCamTarget;
        if (Physics.Raycast(ray, out RaycastHit hit, 500))
        {
            currentCamTarget = hit.point;
        }
        else
        {
            currentCamTarget = transform.TransformPoint(Vector3.forward * 100);
            //print(distanceToHit);
        }

        Debug.DrawLine(ray.origin, currentCamTarget, Color.red);

        return currentCamTarget;
    }
}
