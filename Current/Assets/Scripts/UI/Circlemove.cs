using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circlemove : MonoBehaviour
{

    circle[] m_circles = new circle[7];
    bool m_isrun = false;
    public float speed = 0.25f;
    public float speed2 = 1;



    public void Init()
    {
        m_circles = transform.GetComponentsInChildren<circle>(true);
        for (int i = 0; i < 7; i++)
        {
            m_circles[i].Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 7; i++)
        {
            float elapsedtime = 0;


            Vector3 ori = m_circles[i].transform.localEulerAngles;
            Vector3 target = new Vector3(ori.x, ori.y, ori.z - 80);
            while (elapsedtime < 1)
            {
                elapsedtime += Time.deltaTime * speed;
                m_circles[i].transform.localEulerAngles = Vector3.Lerp(ori, target, elapsedtime);
                float elapsedtime2 = 0;
                while(elapsedtime2<0.5f)
                {
                    elapsedtime2 += Time.deltaTime * speed2;
                }
            }
        }
    }

    private void Awake()
    {
        Init();
        Rotate();
    }
    public void Rotate()
    {
        if (!m_isrun)
        {
            m_isrun = true;
            StartCoroutine(circlemove());
        }
    }

    IEnumerator circlemove()
    {

        for (int i = 0; i < 7; i++)
        {
            m_circles[i].Reload();
        }
        
        while (true)
        {
            for (int i = 0; i < 7; i++)
            {
                float elapsedtime = 0;


                Vector3 ori = m_circles[i].transform.localEulerAngles;
                Vector3 target = new Vector3(ori.x, ori.y, ori.z - 80);
                while (elapsedtime < 1)
                {
                    elapsedtime += Time.deltaTime *60;
                    m_circles[i].transform.localEulerAngles = Vector3.Lerp(ori, target, elapsedtime);
                    yield return null;
                }
            }

        }
    }

}
