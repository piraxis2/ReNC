using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerWindow : Mng
{


    private Text m_name;
    private Text m_lv;
    private Text[] m_info= new Text[2];
    private Button[] m_skillbuttons = new Button[9];
    private Button[] m_buildbuttons = new Button[16];
    private Button[] m_skillupbuttons = new Button[3];
    private Button[] m_unequipbutton = new Button[3];
    private Image[] m_skilltrayicon = new Image[3];
    private Focus[] m_skillfocus = new Focus[9];
    private int m_mana = 0;
    private Image[] m_manaCrystal = new Image[5];
    private Button[] m_buttons = new Button[3];
    private Button m_equipbutton;


    private int m_trayfocus = -1;
    private int m_focus = -1;
    private GameObject m_trayfocusobj;
   
    public override void Init()
    {
        m_name = transform.Find("NAME").GetComponentInChildren<Text>(true);
        m_lv = transform.Find("LV").GetComponent<Text>();
        m_info = transform.Find("InfoTap/Info").GetComponentsInChildren<Text>(true);
        m_skillupbuttons = transform.Find("SkillTap/SkillUpButtons").GetComponentsInChildren<Button>(true);
        for (int i = 0; i < 3; i++)
        {
            Transform t = transform.Find("InfoTap/SkillTray/Skills/Skill" + i.ToString());
            m_unequipbutton[i] = t.GetComponentInChildren<Button>(true);
            int idxx = i;
            m_skillupbuttons[i].onClick.AddListener(() => { SkillUp(idxx); });
        }

        m_equipbutton = transform.Find("InfoTap/Info/EquipButton").GetComponent<Button>();
        m_equipbutton.onClick.AddListener(() => { EquipButton(); });
        int idx = 0;
        foreach (var x in m_unequipbutton)
        {
            int idxx = idx;
            m_skilltrayicon[idx] = x.transform.parent.Find("Icon").GetComponent<Image>();
            m_unequipbutton[idx].onClick.AddListener(() => { UnequipButton(idxx); });
            idx++;
        }
        m_skillbuttons = transform.Find("SkillTap/SkillButtons").GetComponentsInChildren<Button>(true);
        m_skillfocus = transform.Find("SkillTap/SkillButtons").GetComponentsInChildren<Focus>(true);
        m_buildbuttons = transform.Find("BuildTap").GetComponentsInChildren<Button>(true);
        idx = 0;
        foreach (var x in m_skillbuttons)
        {
            int idxx = idx;
            m_skillbuttons[idx].onClick.AddListener(() => { SkillButton(idxx); });
            idx++;
        }
        idx = 0;
        foreach (var x in m_buildbuttons)
        {
            int idxx = idx;
            m_buildbuttons[idx].onClick.AddListener(() => { BuildUp(idxx); });
            idx++;
        }
    
        m_manaCrystal = transform.Find("InfoTap/SkillTray/ManaTray/Full").GetComponentsInChildren<Image>(true);
        PageReLoad();
    
    }

    private void Manaview()
    {
        for (int i = 0; i < 5; i++)
        {
            if (m_mana == 0)
                return;

            int manaval = m_mana - ((i + 1) * 2);

            if (manaval >= 1)
            {
                m_manaCrystal[i].fillAmount = 1;
                continue;
            }
            if (manaval < -1)
            {
                m_manaCrystal[i].fillAmount = 0;
                return;
            }
            switch (manaval)
            {
                case -1: m_manaCrystal[i].fillAmount = 0.5f; break;
                case 0: m_manaCrystal[i].fillAmount = 1; break;
            }

        }
    }

    
    public void SkillButton(int idx)
    {
        PlayerSkill ps = TableMng.Instance.Table(TableType.PlayerSkillTable, idx)as PlayerSkill;
        m_info[0].text = ps.m_name;
        m_info[1].text = ps.m_info;
        m_focus = idx;
    }

    public void SkillUp(int idx)
    {
        PlayerData.Instance.SkillUP(idx);
        SkillReload();
    }

    public void BuildUp(int idx)//여기서부터 ////////////////////////////
    {
        int num = (int)idx / 4;
        int num2 = idx % 4;
        PlayerData.Instance.Build[num]++;
        if (PlayerData.Instance.Build[num] > 4)
        {
            PlayerData.Instance.Build[num] = 4;
        }
        PageReLoad();
    }

    public void EquipButton()
    {
        if (m_focus < 0 && m_focus > 8)
            return;

        if (PlayerData.Instance.SkillTray.Count >= 3)
            return;

        int num = m_focus % 3;
        int num2 = PlayerData.Instance.Skill((int)m_focus / 3);
        if (PlayerData.Instance.Skill((int)m_focus / 3) <= num)
            return;

        PlayerData.Instance.SkillTray.Add(m_focus);
        SkillTrayReload();
    }

    public void UnequipButton(int idx)
    {
        PlayerData.Instance.SkillTray.RemoveAt(idx);
        SkillTrayReload();
    }


    public void PageOpen()
    {
        gameObject.SetActive(true);
        PageReLoad();

        StartCoroutine(IETooltip());
    }

    public void PageClose()
    {
        gameObject.SetActive(false);
        StopCoroutine(IETooltip());
    }
   
    public void SkillTrayReload()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PlayerData.Instance.SkillTray.Count - 1 >= i)
            {
                int temp = PlayerData.Instance.SkillTray[i];
                if (PlayerData.Instance.Icons(i) != null)
                    m_skilltrayicon[i].sprite = PlayerData.Instance.Icons(i);
                PlayerSkill ps = TableMng.Instance.Table(TableType.PlayerSkillTable, temp) as PlayerSkill;
                m_skilltrayicon[i].transform.parent.Find("Cost/Text").GetComponent<Text>().text = ps.m_cost.ToString();
            }
            else
            {
                m_skilltrayicon[i].sprite = SpriteMng.s_blanksprite;
                m_skilltrayicon[i].transform.parent.Find("Cost/Text").GetComponent<Text>().text = string.Empty;
            }
        }
    }


    public void SkillReload()
    {
        int num = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (PlayerData.Instance.Skill(i) > j)
                { m_skillbuttons[num].transform.Find("OFF").gameObject.SetActive(false); }
                num++;
            }
        }
    }

    public void BuildReLoad()
    {
        int num = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (PlayerData.Instance.Build[i] > j)
                {
                    m_buildbuttons[num].GetComponent<Image>().sprite = SpriteMng.s_someicons[2].sprite;
                }
                num++;
            }
        }
    }


    public void PageReLoad()
    {
        m_lv.text = "PLAYER LV" + PlayerData.Instance.LV.ToString();
        SkillTrayReload();
        SkillReload();
        BuildReLoad();
    }

    private IEnumerator IETooltip()
    {
        List<RectTransform> rt = new List<RectTransform>();
        foreach (var x in m_buildbuttons)
        {
            rt.Add(x.GetComponent<RectTransform>());
        }

        while (true)
        {
            bool boxoff = false;
            int idx = 0;
            foreach (var x in rt)
            {
                if (CalculateBounds(x, x.lossyScale.x).Contains(Input.mousePosition))
                {
                    Build bt = TableMng.Instance.Table(TableType.BuildTable, idx) as Build;
                    ToolTipBox.Instance.SetBox(bt.m_name, bt.m_info, "COST : " + bt.m_cost.ToString(), Input.mousePosition);
                    boxoff = true;
                    break;
                }
                idx++;
            }

            if(!boxoff)
            {
                ToolTipBox.Instance.OffBox();
            }


            yield return null;
        }
    }

    Bounds CalculateBounds(RectTransform transform, float uiScaleFactor)
    {
        Bounds bounds = new Bounds(transform.position, new Vector3(transform.rect.width, transform.rect.height, 0.0f) * uiScaleFactor);

        if (transform.childCount > 0)
        {
            foreach (RectTransform child in transform)
            {
                Bounds childBounds = new Bounds(child.position, new Vector3(child.rect.width, child.rect.height, 0.0f) * uiScaleFactor);
                bounds.Encapsulate(childBounds);
            }
        }

        return bounds;
    }
}
