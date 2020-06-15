using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingScript : MonoBehaviour
{
    public Transform pivot;
    public GameObject bullet;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
           Instantiate(bullet,pivot.position, pivot.rotation);
           
        }
    }
}
