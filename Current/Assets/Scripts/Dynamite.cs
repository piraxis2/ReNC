using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : PixelFx 
{
   public int speed = 500;
    

    void Update()
    {
        if (!gameObject.activeInHierarchy)
            return;


        transform.Rotate(0, 0, (Time.deltaTime) * -speed);


    }
}
