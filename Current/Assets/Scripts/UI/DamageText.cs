using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI[] m_textmesh= new TMPro.TextMeshProUGUI[2];
    private bool m_isrun = false;
    private int m_ID;
    private Color m_startco;
    private Color m_startco1;
    private Color m_startred = new Color(1f, 0f, 0.1f);
    private Color m_startred1= new Color(0.86f, 0, 0.1f);
    private Color m_startgreen = new Color(0.4f, 1f, 0);
    private Color m_startgreen1 = new Color(0.8f, 1f, 0.5f);
    public int ID
    {
        get { return m_ID; }
    }

    public void SetID(int val)
    {
        m_ID = val;
    }

    public bool Isrun
    {
        get { return m_isrun; }
    }


    public void Init()
    {
        m_textmesh = GetComponentsInChildren<TMPro.TextMeshProUGUI>(true);

    }

    public void GetDamage(int x, BaseChar pos, string text = null)
    {
        m_startco = m_startred;
        m_startco1 = m_startred1;
        m_textmesh[1].fontSize = 30;
        if (text != null)
            m_textmesh[1].SetText(text);
        else
            m_textmesh[1].SetText("");

        if (Mathf.Abs(x) > 30)
        {
            m_textmesh[0].fontSize = 60;
            m_textmesh[0].SetText("{0}!", Mathf.Abs(x));
        }
        else
        {
            m_textmesh[0].fontSize = 40;
            m_textmesh[0].SetText("{0}", Mathf.Abs(x));
        }

        if(x<0)
        {
            m_startco = m_startgreen;
            m_startco1 = m_startgreen1;
        }

        if (pos != null)
            transform.position = Camera.main.WorldToScreenPoint(pos.transform.position) + (new Vector3(0, 50, 0));

        gameObject.SetActive(true);
        m_isrun = true;

        if (m_textmesh[1].text == "MISS")
            m_textmesh[0].SetText("");

        if (m_textmesh[1].text == "DEATHSTRIKE")
            m_textmesh[0].SetText("");

        StartCoroutine(IEDamageView(pos));
    }

    IEnumerator IEDamageView(BaseChar pos)
    {
        float elapsedtime = 0;
        Color startcolor = m_startco; 
        Color startcolor1 = m_startco1; 
        m_textmesh[0].color = startcolor;
        m_textmesh[1].color = startcolor1;
        float text0 = m_textmesh[0].fontSize;
        float text1 = m_textmesh[1].fontSize;

        Vector3 startpos = transform.position;
        Vector3 p1 = startpos + new Vector3(0, 70, 0);
        Vector3 p2  = startpos + new Vector3(-20, 70, 0);
        Vector3 targetpos = startpos + new Vector3(-20, 0, 0);

        bool stop = false;
        while (!stop)
        {
            if(pos!=null)
                transform.position = Camera.main.WorldToScreenPoint(pos.transform.position) + (new Vector3(0, 50, 0));

            startpos = transform.position;
            p1 = startpos + new Vector3(0, 70, 0);
            p2 = startpos + new Vector3(-20, 70, 0);
            targetpos = startpos + new Vector3(-20, 0, 0);
            elapsedtime += Time.deltaTime * 1.5f;
            Mathf.Clamp01(elapsedtime);
            m_textmesh[1].fontSize = Mathf.Lerp(text1, 5, elapsedtime);
            m_textmesh[0].fontSize = Mathf.Lerp(text0, 15, elapsedtime);
            transform.GetChild(0).transform.position = MathHelper.BezierCurve(startpos, p1, p2, targetpos, elapsedtime);
 
            if(elapsedtime >=1)
            {
                stop = true;
            }

            yield return null;
        }

        m_isrun = false;
        if(m_ID>41)
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
        m_textmesh[0].color = startcolor;
        m_textmesh[1].color = startcolor1;
        yield return null;
    }

}
