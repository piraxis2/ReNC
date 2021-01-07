using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunRotate : MonoBehaviour
{




    public int speed = 500;

    void Update()
    {

        if (!gameObject.activeInHierarchy)
            return;


        transform.Rotate(0, 0, (Time.deltaTime)*-speed);



    }
}
