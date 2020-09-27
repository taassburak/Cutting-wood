using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FORCE : MonoBehaviour
{
    Rigidbody fizik;
    public float z_axis=-500f;
    public float y_axis = -30;
    float forcetime = 0;
    public float waitingtime = 0.7f;
    
    void Start()
    {
        fizik = GetComponent<Rigidbody>();
        fizik.AddForce(0, y_axis, z_axis);

    }
    void Update()
    {
        kuvvet();
    }
    void kuvvet()
    {
        
            forcetime += Time.deltaTime;
            if (forcetime > waitingtime)
            {
                fizik.AddForce(0, y_axis / 2, z_axis / 2);
                forcetime = 0;
            }
        
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="zemin2")
        {
            Destroy(gameObject,2);
        }
    }

}
