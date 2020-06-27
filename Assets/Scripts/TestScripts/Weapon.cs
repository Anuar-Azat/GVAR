using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shotpoint;
    public Transform targetLook;
    public GameObject MainCamera;
    public GameObject Decal;

    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 origin = shotpoint.position;
        Vector3 dir = targetLook.position;
     //   RaycastHit hit;
        Debug.DrawLine(origin, dir, Color.black);
        Debug.DrawLine(MainCamera.transform .position, dir, Color.black);

    }
}
