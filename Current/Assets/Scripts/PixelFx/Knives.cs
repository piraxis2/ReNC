using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knives : PixelFx 
{

    private SpriteRenderer[] m_knife = new SpriteRenderer[30];

    private int m_count2 = 0;
    private int m_rotate = 0;
    private int m_speed = 50;
    private float m_plus = 0.025f;

    public override void Init()
    {
           m_knife = GetComponentsInChildren<SpriteRenderer>(true);
    }



    private void Awake()
    {
        m_knife = GetComponentsInChildren<SpriteRenderer>(true);
    }


    public void ActiveStart()
    {

        for (int i = 0; i < m_knife.Length; i++)
        {
            m_knife[i].transform.localPosition = Vector3.zero;
        }

        float voke = m_plus;
        Knife(); 
        for (int i = 0; i < 14; i++)
        {
            Invoke("Knife", voke);
            voke += m_plus;
        }
        float voke2 = 0.125f;
        for (int i = 0; i < 15; i++)
        {
            Invoke("Knife", voke2);
            voke2 += m_plus;
        }

        Invoke("ShutActive", voke2 + 0.125f);



    }

    private void Knife()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IEKnivesSpread(m_knife[m_count2]));

        m_count2++;
        if (m_count2 >= 30)
        {
            m_count2 = 0;
        }
    }


    private IEnumerator IEKnivesSpread(SpriteRenderer knife)
    {
        bool stop = false;
        float elapsedtime = 0;
        knife.gameObject.SetActive(true);
        Vector3 pos = knife.transform.localPosition;
        Vector3 end = new Vector3(pos.x, pos.y + 1f);

        while (!stop)
        {

            elapsedtime += Time.deltaTime * 8f;

            knife.transform.localPosition = Vector3.Lerp(pos, end, elapsedtime);

            if (elapsedtime >= 1)
            {
                stop = true;
                knife.gameObject.SetActive(false);
                knife.transform.localPosition = pos;
            }


            yield return null;
        }


        yield return null;
    }

}
