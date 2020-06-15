using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject OldObj;
    public GameObject NewObj;
    public int HealthObj = 30;

    public void TakeDamage(int damage) 
    {
        HealthObj -= damage;
        if (HealthObj <= 0)
        {
            Die();
        }

    }
    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.collider.tag == "BULLET")
    //    {
    //        Health--;
    //    }
    //}
    void Die() 
    {
        Destroy(gameObject);
        NewObj.SetActive(true);
        Destroy(NewObj, 4f);
    }
    
   
}
