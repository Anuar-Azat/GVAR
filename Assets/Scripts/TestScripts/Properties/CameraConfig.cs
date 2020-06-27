using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Camera/Config")]
public class CameraConfig : ScriptableObject
{
    public float turnSmooth;  // сглаживание
    public float pivotSpeed; 
    public float Y_Rot_Speed;
    public float X_Rot_speed;
    public float minAngle;
    public float maxAngle;
    public float normalZ;
    public float normalX;
    public float normalY;
    public float aimZ;  
    public float aimX;



}
