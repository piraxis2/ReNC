using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{

    public void Shutactive()
    {
        GetComponentInParent<PixelFx>().ShutActive();
        gameObject.SetActive(false);

    }
}
