using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell1 : MonoBehaviour
{
    public float speed;
    public float destroyTime;

    public int damage=10;
    void Start()
    {
        Invoke("DestroyShell", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void DestroyShell()
    {
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("+");
        DestroyObj enemy = collision.gameObject.GetComponent<DestroyObj>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
    

}
