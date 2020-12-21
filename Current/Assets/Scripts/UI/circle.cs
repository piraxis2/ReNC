using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle : MonoBehaviour
{
    public Vector3 m_orilotate;
    // Start is called before the first frame update
    public void Init()
    {
        m_orilotate = transform.localEulerAngles;
    }
    public void Reload()
    {
        transform.localEulerAngles = m_orilotate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
