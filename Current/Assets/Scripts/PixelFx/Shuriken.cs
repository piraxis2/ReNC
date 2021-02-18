using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private int speed = 1000;

    // Update is called once per frame
    void Update()
    {
       
        if(!gameObject.activeInHierarchy)
        {
            return;
        }

        transform.Rotate(0, 0, (Time.deltaTime) * -speed);

    }
}
