using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownView : MonoBehaviour
{
    BackGroundMove m_buildings;
    float m_elapsedtime = 0;
    public float m_speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        m_buildings = GetComponentInChildren<BackGroundMove>();
        m_buildings.m_scrollSpeed = 0.02f;
    }


    IEnumerator IELeftRight()
    {
        while(true)
        {
            m_elapsedtime += Time.deltaTime * m_speed;

            if (m_elapsedtime > 1)
            {
                m_elapsedtime = 0;
                m_buildings.m_swap = true;
            }
            yield return null;
        }
    }
}
