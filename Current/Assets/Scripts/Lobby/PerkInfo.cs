using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkInfo : MonoBehaviour
{
    Image m_icon;
    Text m_perkname;
    Text m_perkinfo;
    public int m_targetidx = -1;

    public void PerkInfoInit()
    {
        m_icon = transform.Find("PerkImage").GetComponent<Image>();
        m_perkname = transform.Find("PerkName").GetComponent<Text>();
        m_perkinfo = transform.Find("PerkInfo").GetComponent<Text>();
    }


    public void PopUpInfo(int idx)
    {
        gameObject.SetActive(true);
        Perk perk = TableMng.Instance.Table(TableType.PERKTable, idx) as Perk;
        m_perkname.text = perk.m_name;
        m_perkinfo.text = perk.m_option + '\n' + perk.m_option2;
        m_targetidx = idx;
        m_icon.sprite = SpriteMng.s_perkicons[1, idx];
    }


}
