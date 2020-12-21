using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerINFO : MonoBehaviour
{


    private static PlayerINFO s_playerinfo;
    public static PlayerINFO Instatnce
    {
        get
        {
            if (s_playerinfo == null)
            {
                s_playerinfo = GameObject.Find("Main/Canvas/PLAYERINFO").GetComponent<PlayerINFO>();
                s_playerinfo.Init();
            }

            return s_playerinfo;
        }
    }



    private Image m_xpgage;
    private Text m_xptext;
    private Image m_HP;
    private Text m_lv;
    private Text m_hptext;
    private Text m_gold;


    private void Init()
    {
        GameObject xp = transform.Find("XP").gameObject;
        m_xpgage = xp.transform.Find("BAR/gage").GetComponent<Image>();
        m_xptext = xp.transform.Find("BAR/TEXT").GetComponent<Text>();
        GameObject LV = transform.Find("LV").gameObject;
        m_lv = LV.transform.Find("Text").GetComponent<Text>();
        m_HP = LV.transform.Find("Bar/gage").GetComponent<Image>();
        m_hptext = LV.transform.Find("Bar/Text").GetComponent<Text>();
        m_gold = transform.Find("GOLD/Text").GetComponent<Text>();
        GoldIncomeBox box = transform.GetComponentInChildren<GoldIncomeBox>(true);
        GameObject gold = transform.Find("GOLD").gameObject;
        MouseOver.Instance.MouseOverON(gold, box);
        InfoUpdate();

    }

    public void BUYEXP()
    {
        if (PlayerData.Instance.GOLD - 4 < 0)
            return;

        PlayerData.Instance.ExpUp(4);
        PlayerData.Instance.GoldCunsume(4);
    }

    public void HPupdate()
    {
        m_lv.text = PlayerData.Instance.LV.ToString();
        m_HP.fillAmount =( (float)PlayerData.Instance.HP / 100);
        m_hptext.text = PlayerData.Instance.HP.ToString() + '/' + "100";
    }

    public void XPupdate()
    {
        if (PlayerData.Instance.LV == 9)
        {
            m_xpgage.fillAmount = 1;
            m_xptext.text = "MAX";
            return;
        }

        m_xpgage.fillAmount = ((float)PlayerData.Instance.EXP / PlayerData.Instance.EXPTABLE[PlayerData.Instance.LV - 1]);
        m_xptext.text = PlayerData.Instance.EXP.ToString() + '/' + PlayerData.Instance.EXPTABLE[PlayerData.Instance.LV - 1].ToString();
    }

    public void Goldupdate()
    {
        m_gold.text = PlayerData.Instance.GOLD.ToString();
    }


    public void InfoUpdate()
    {
        Goldupdate();
        XPupdate();
        HPupdate();
    }


}
