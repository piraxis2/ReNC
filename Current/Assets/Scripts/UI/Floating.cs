using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{

    Vector3 m_oripos;
    Vector3 m_targetpos;
    float m_elapsedtime = 0;
    public float m_speed = 1;

    void Awake()
    {
        m_oripos = transform.position;
        m_targetpos = m_oripos + new Vector3(0, 10, 0);
    }

    void Update()
    {

        m_elapsedtime += Time.deltaTime*m_speed;
        transform.position = Vector3.Lerp(m_oripos, m_targetpos, m_elapsedtime);
        if(m_elapsedtime>=1)
        {
            m_elapsedtime = 0;
            Vector3 temp = m_oripos;
            m_oripos = m_targetpos;
            m_targetpos = temp;
        }

    }
}
