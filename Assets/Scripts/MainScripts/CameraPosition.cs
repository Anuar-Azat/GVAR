using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour
{
    public Transform Rotate;
    public Transform Target;
    public float speedRotateCamera = 5f;

    void Start()
    {
        transform.parent = null;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, Target.rotation, speedRotateCamera);
    }

    void OnGUI()
    {
        //GUI.DrawTexture(camera_aim_position, camera_aim);
    }

    public Vector3 GetAimPoint()
    /// <summary>
    /// Функция поиска точки экранного прицела
    /// </summary>
    /// <returns>Vector3 точки в пространстве</returns>
    {
        Ray ray = new Ray(transform.position, transform.forward);
        float distanceToHit = 0f;
        Vector3 currentCamTarget;
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
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
