using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recruit : Mng
{

    private Text m_name;
    private Text m_lv;
    private Text m_exptext;
    private Text m_page;
    private Image[] m_expimage;
    private Image m_face;
    private Image m_heroclass;
    private Hero m_hero;
    private int m_bookidx = 0;
    private static int m_rimitidx = 1;
    private Text[] m_stateviewtxts = new Text[7];
    private SpriteRenderer[] m_classsprite = new SpriteRenderer[5];
    private char m_statechangetoken = '-';
    private CardMng m_cardmng;
    private GameObject m_emptyview;

    private Image m_skillicon;
    private Text[] m_skilltext = new Text[3];
    private Text m_passives;
    private Transform m_detailinfo;
    private Text m_DSinfostats;

    private ItemInfo m_iteminfo;

    private Button[] m_equipmentslots = new Button[3];
    private Image[] m_equipmentimages = new Image[3];


    public override void Init()
    {
        m_name = transform.Find("CharacterData/Name").GetComponentInChildren<Text>(true);
        m_lv = transform.Find("CharacterData/LV").GetComponent<Text>();
        Transform obj = transform.Find("CharacterData/Exp");
        m_exptext = obj.GetComponentInChildren<Text>(true);
        m_expimage = obj.GetComponentsInChildren<Image>(true);
        m_face = transform.Find("CharacterData/FaceTray/Face").GetComponent<Image>();
        m_heroclass = transform.Find("CharacterData/Class").GetComponent<Image>();
        obj = transform.Find("BaseStats");
        m_stateviewtxts = obj.GetComponentsInChildren<Text>(true);
        obj = transform.Find("Page");
        m_page = obj.GetComponent<Text>();
        m_classsprite = GetComponentsInChildren<SpriteRenderer>(true);
        m_rimitidx = NewbieMng.s_heros.Count;
        m_cardmng = FindObjectOfType<CardMng>();
        m_emptyview = transform.Find("Empty").gameObject;
        StatusInit();
        EquipmentInit();

        m_iteminfo = GetComponentInChildren<ItemInfo>();
        m_iteminfo.ItemInfoInit();
        Button iteminfoclose = m_iteminfo.GetComponentInChildren<Button>();
        iteminfoclose.onClick.AddListener(() => { Sibilding(3); });
    }
    private void StatusInit()
    {
        m_detailinfo = transform.Find("Status/DetailInfo");
        m_DSinfostats = m_detailinfo.Find("Info2").GetComponent<Text>();
        m_passives = transform.Find("Status/Passives").GetComponent<Text>();
        m_skilltext = transform.Find("Status/Skill").GetComponentsInChildren<Text>();
        m_skillicon = transform.Find("Status/Skill/SkillIcon").GetComponent<Image>();

    }
    public static void PageAddRemove(int val)
    {
        m_rimitidx += val;
    }

    public void PopUp()
    {
        PageOpen(m_bookidx);
    }

    public void NextPage()
    {
        m_bookidx = m_bookidx + 1 > m_rimitidx - 1 ? 0 : m_bookidx + 1;
        PageOpen(m_bookidx);
    }
    public void PrevPage()
    {
        m_bookidx = m_bookidx - 1 < 0 ? m_rimitidx - 1 : m_bookidx - 1;
        PageOpen(m_bookidx);
    }

    public void Hire()
    {
        if (NewbieMng.s_heros.Count == 0)
            return;

        Hero hero = NewbieMng.s_heros[m_bookidx];
        NewbieMng.s_heros.RemoveAt(m_bookidx);
        NewbieMng.Reload();
        m_cardmng.AddCard(hero);
        PageAddRemove(-1);
        if(NewbieMng.s_heros.Count==0)
        {
            m_emptyview.SetActive(true);
            m_page.text = "Empty";
            return;
        }
        NextPage();

    }


    private void EquipmentInit()
    {
        Transform obj = transform.Find("BaseEquipment");
        m_equipmentimages[0] = obj.Find("Weapon/Image").GetComponent<Image>();
        m_equipmentimages[1] = obj.Find("Armor/Image").GetComponent<Image>();
        m_equipmentimages[2] = obj.Find("Trinket/Image").GetComponent<Image>();
        m_equipmentslots[0] = obj.Find("Weapon").GetComponent<Button>();
        m_equipmentslots[1] = obj.Find("Armor").GetComponent<Button>();
        m_equipmentslots[2] = obj.Find("Trinket").GetComponent<Button>();
        for (int i = 0; i < 3; i++)
        {
            int idxx = i;
            m_equipmentslots[i].onClick.AddListener(() => { m_iteminfo.ClickEquipment(idxx); Sibilding(i); });
        }

    }

    public void EquipMentSlotReset()
    {
        for (int i = 0; i < 3; i++)
        {
            if (m_hero.Status.EquipMent[i] == null)
                m_equipmentimages[i].sprite = SpriteMng.s_blanksprite;
            else
                m_equipmentimages[i].sprite = m_hero.Status.EquipMent[i].m_sprite;
        }
    }

    public void StatusWindowReset()
    {
        m_DSinfostats.text =
            m_hero.Status.LV.ToString() + '\n' +
            string.Format("{0}({1}){2}", m_hero.Status.ORILife, m_hero.Status.MaxLife - m_hero.Status.ORILife, m_hero.Status.Token("LIFE"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.Status.ORIMana, m_hero.Status.Mana - m_hero.Status.ORIMana, m_hero.Status.Token("MANA"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.Status.ORIAD, m_hero.Status.AD - m_hero.Status.ORIAD, m_hero.Status.Token("AD"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.Status.ORIAP, m_hero.Status.AP - m_hero.Status.ORIAP, m_hero.Status.Token("AP"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.Status.ORIDF, m_hero.Status.DF - m_hero.Status.ORIDF, m_hero.Status.Token("DF"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.Status.ORIAS, m_hero.Status.AS - m_hero.Status.ORIAS, m_hero.Status.Token("AS"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.Status.ORILPS, m_hero.Status.LPS - m_hero.Status.ORILPS, m_hero.Status.Token("LPS"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.Status.ORIMPS, m_hero.Status.MPS - m_hero.Status.ORIMPS, m_hero.Status.Token("MPS"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.Status.ORICLI, m_hero.Status.CLI - m_hero.Status.ORICLI, m_hero.Status.Token("CLI"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.Status.ORIAVD, m_hero.Status.AVOID - m_hero.Status.ORIAVD, m_hero.Status.Token("AVD"))
            + '\n' +
            m_hero.Status.Range.ToString();

        m_passives.text = "";
        for (int i = 0; i < TableMng.Instance.TableLength(TableType.PassiveTable); i++)
        {
            if (m_hero.Status.Passive(i))
            {
                m_passives.text += (TableMng.Instance.Table(TableType.PassiveTable, i) as Passive).m_name + "\n";
            }
        }
        Skillinfo skillinfo = (TableMng.Instance.Table(TableType.SkillInfoTable, (int)m_hero.Skill) as Skillinfo);
        m_skilltext[0].text = skillinfo.m_name;
        m_skilltext[1].text = skillinfo.m_text;
        m_skilltext[2].text = skillinfo.m_info;
        m_skillicon.sprite = skillinfo.m_icon;

    }

    private void Sibilding(int idx)
    {
        if (idx == 3)
        {
            m_iteminfo.gameObject.SetActive(false);
            m_detailinfo.gameObject.SetActive(true);
        }
        else if (idx < 3)
        {
            if (m_hero.Status.EquipMent[idx] == null)
                return;

            m_detailinfo.gameObject.SetActive(false);
            m_iteminfo.gameObject.SetActive(true);
        }
    }

    public void PageOpen(int idx)
    {
        gameObject.SetActive(true);
        if (NewbieMng.s_heros.Count == 0)
        {
            m_emptyview.SetActive(true);
            m_page.text = "Empty";
            return;
        }
        else
        {
            m_emptyview.SetActive(false);
        }
        m_hero = NewbieMng.FindHero(idx);

        m_name.text = m_hero.Status.Name;
        m_lv.text = m_hero.Status.LV.ToString();
        m_exptext.text = string.Format("EXP : {0}/{1}", m_hero.Status.EXP, 100);
        m_expimage[1].fillAmount = (float)m_hero.Status.EXP / 100;
        m_face.sprite = m_hero.Face;
        switch (m_hero.ClassType)
        {
            case ClassType.Hunter: m_heroclass.sprite = SpriteMng.s_classimage[(int)ClassType.Hunter].sprite; break;
            case ClassType.Knight: m_heroclass.sprite = SpriteMng.s_classimage[(int)ClassType.Knight].sprite; break;
            case ClassType.Mage: m_heroclass.sprite = SpriteMng.s_classimage[(int)ClassType.Mage].sprite; break;
            case ClassType.Warrior: m_heroclass.sprite = SpriteMng.s_classimage[(int)ClassType.Warrior].sprite; break;
        }
        m_stateviewtxts[1].text = string.Format("{0}/{1}", m_hero.Status.Life, m_hero.Status.Life);
        m_stateviewtxts[2].text = string.Format("BASE:{0}", m_hero.Status.BaseMana);
        m_stateviewtxts[3].text = string.Format("{0}({1}) {2}", m_hero.Status.AD, 0, m_statechangetoken);
        m_stateviewtxts[4].text = string.Format("{0}({1}) {2}", m_hero.Status.AP, 0, m_statechangetoken);
        m_stateviewtxts[5].text = string.Format("{0}({1}) {2}", m_hero.Status.DF, 0, m_statechangetoken);
        m_stateviewtxts[6].text = string.Format("{0}({1})", m_hero.Status.AS, 0);
        m_page.text = string.Format("{0}/{1}", m_bookidx+1, m_rimitidx);

        StatusWindowReset();
        EquipMentSlotReset();
    }
}
