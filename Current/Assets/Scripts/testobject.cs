using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testobject : MonoBehaviour
{
    Vector3[] targets = new Vector3[4];
    Vector3[] t2 = new Vector3[4];
    void Start()
    {

        targets[0] = transform.position;
        targets[1] = transform.position + new Vector3(-3, 10, 0);
        targets[2] = Vector3.zero + new Vector3(-3, 10, 0);
        targets[3] = Vector3.zero;

        t2[0] = transform.position;
        t2[1] =  transform.position + new Vector3(-3, 10, 0);
        t2[2]=  new Vector3(3,3,0) + new Vector3(-3, 10, 0);
        t2[3] = new Vector3(3, 3, 0);

  

    }

}
