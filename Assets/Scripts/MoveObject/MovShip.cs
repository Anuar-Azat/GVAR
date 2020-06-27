using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovShip : MonoBehaviour
{
    public GameObject platform;
    public float x;
    public float y;
    public float z;
    public float speed = 1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(platform.transform.position, new Vector3(x, y, z), speed * Time.deltaTime);
    }
}
