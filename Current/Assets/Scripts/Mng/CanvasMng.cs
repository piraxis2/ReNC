using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMng : MonoBehaviour
{

    private static CanvasMng s_canvasMng;

    public static CanvasMng Instance
    {
        get
        {
            if (s_canvasMng == null)
            {
                s_canvasMng = GameObject.Find("Main/StateUI").GetComponent<CanvasMng>();
                s_canvasMng.Init();
            }
            return s_canvasMng;
        }
    }

    private List<StateBar> m_ContainedStatebar = new List<StateBar>();
    private List<StateBar> m_statebars = new List<StateBar>();
    private Text m_globalturn;

    private List<DamageText> m_damagetexts = new List<DamageText>();
    private DamageText m_damagetext;

    private List<Tail> m_tails = new List<Tail>();

    private int m_onfieldindexer = 0;

    public void Init()
    {
        m_globalturn = transform.GetChild(2).GetComponentInChildren<Text>();
        m_statebars.AddRange(GetComponentsInChildren<StateBar>(true));

        m_damagetext = Resources.Load("Prefab/Damage") as DamageText;
        m_damagetexts.AddRange(GetComponentsInChildren<DamageText>(true));
        int count = 0;
        foreach (var x in m_damagetexts)
        {
            x.SetID(count);
            x.Init();
            count++;
        }


    }
    public void SetTurn(int val)
    {
        m_globalturn.text ="CurrTurn : "+ val.ToString();
    }

    public void LoadStateBar()
    {
        
        for (int i = 0; i < CharMng.Instance.TotalHeros.Count; i++)
        {
            m_statebars[i].gameObject.SetActive(true);
            m_statebars[i].transform.position = Camera.main.WorldToScreenPoint(CharMng.Instance.TotalHeros[i].transform.position);
            m_statebars[i].Charset(CharMng.Instance.TotalHeros[i], m_onfieldindexer);
            m_onfieldindexer++;
        }
    }

    public void ALLOffStateBar()
    {
        int idx = 0;
        foreach(var x in CharMng.Instance.TotalHeros)
        {
            m_statebars[idx].CharOut();
            idx++;
        }
        m_onfieldindexer = 0;
    }

    public void AddCharacter(BaseChar chara)
    {

        foreach(var x in m_statebars)
        {
            if (x.gameObject.activeSelf)
                continue;

            x.gameObject.SetActive(true);
            x.transform.position = Camera.main.WorldToScreenPoint(chara.transform.position);
            x.Charset(chara, m_onfieldindexer);

        }
    }

    public void DamageCall(int dam, BaseChar pos, string text = null)
    {
        foreach (var x in m_damagetexts)
        {
            if (x.Isrun)
                continue;

            x.GetDamage(dam, pos, text);
            return;
        }
        DamageText damageText = Instantiate(m_damagetext);
        m_damagetexts.Add(damageText);
        damageText.GetDamage(dam, pos, text);
    }


}
