using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    public float Speed;
    Vector3 lastPos;  // позиция пули в прошл кадре
    public GameObject Decal;
    //void Start()
    //{
    //    lastPos = transform.position;
    //}

    // Update is called once per frame
    void Update()
    {
       
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        //   RaycastHit hit;
        //    Debug.DrawLine(lastPos, transform.position);
        //if (Physics.Linecast(lastPos, transform.position, out hit))
        //{

        //        GameObject D = Instantiate<GameObject>(Decal);


        //        D.transform.rotation = Quaternion.LookRotation(-hit.normal);
        //    D.transform.position = hit.point + hit.normal * 0.001f;
        //    D.transform.Rotate(-90, 0, 0);

        //        Destroy(gameObject);
        //    Destroy(D, 2);

        //    lastPos = transform.position;
        //}

       

    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);


    }
}
