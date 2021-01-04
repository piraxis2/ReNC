using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterBook : MonoBehaviour
{
    private static CharacterBook s_characterbook;

    public static CharacterBook Instance
    {
        get
        {

            if (s_characterbook == null)
            {
                s_characterbook = GameObject.Find("Main/Canvas/Books/CharacterBook").GetComponent<CharacterBook>();
                s_characterbook.Init();
            }

            return s_characterbook;
        }
    }


    private Text m_name;
    private Text m_lv;
    private Text m_exptext;
    private Text m_page;
    private Image[] m_expimage;
    private Image m_face;
    private Image m_heroclass;
    private Hero m_hero;
    public  int m_bookidx = 0;
    private  int m_rimitidx = 1;
    private Text[] m_stateviewtxts = new Text[7];
    private Button[] m_equipmentslots = new Button[3];
    private Image[] m_equipmentimages = new Image[3];
    private List<Button> m_slots = new List<Button>();
    private List<Image> m_itemimages = new List<Image>();
    private List<Button> m_perks = new List<Button>();
    private List<Image> m_perkImages = new List<Image>();
    private ItemInfo m_iteminfo;
    public ItemInfo ItemInfo
    {
        get { return m_iteminfo; }
    }
    private PerkInfo m_perkinfo;
    private Transform m_detailinfo;
    private Text m_DSinfostats;
    private Button m_equipunequip;
    private Text m_equipbuttontext;
    private Text m_perkpoint;
    private Text[] m_skilltext = new Text[3];
    private Text m_passives;
    private Transform m_clip;
    private Image m_skillicon;
    Button[] m_clips = new Button[3];




    public void Init()
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
        obj = transform.Find("Clips");
        m_clip = obj;
        m_detailinfo = obj.transform.Find("Status/DetailInfo");
        m_DSinfostats = m_detailinfo.Find("Info2").GetComponent<Text>();
        ItemSlotInit();
        StatusInit();
        PerkInit();
        EquipmentInit();
        ClipInit();
        m_rimitidx = CardMng.Instance.Deck.Count;

        if (MainMng.s_seceneloadcount < 1)///temp
        {
            Inventory.instance.AddItem(TableMng.Instance.Table(TableType.ITEMTable, 0) as Item);
            Inventory.instance.AddItem(TableMng.Instance.Table(TableType.ITEMTable, 1) as Item);
        }
    }


    public void ClipInit()
    {

        m_clips[0] = m_clip.Find("Status").GetComponent<Button>();
        m_clips[1] = m_clip.Find("Perk").GetComponent<Button>();
        m_clips[2] = m_clip.Find("Equip").GetComponent<Button>();

        for (int i = 0; i < 3; i++)
        {
            int idx = i;
            m_clips[i].onClick.AddListener(() => { Sibilding(idx); });
        }
        Sibilding(0);

    }

    private void Sibilding(int idx)
    {
        m_clips[idx].transform.SetAsLastSibling();
        m_iteminfo.gameObject.SetActive(false);
        m_perkinfo.gameObject.SetActive(false);
        m_detailinfo.gameObject.SetActive(false);
        if (idx == 0)
            m_detailinfo.gameObject.SetActive(true);

    }

    private void StatusInit()
    {
        m_passives = m_clip.transform.Find("Status/Passives").GetComponent<Text>();
        m_skilltext = m_clip.transform.Find("Status/Skill").GetComponentsInChildren<Text>();
        m_skillicon = m_clip.transform.Find("Status/Skill/SkillIcon").GetComponent<Image>();
    }


    private void PerkInit()
    {
        m_perkpoint = m_clip.transform.Find("Perk/Num").GetComponentInChildren<Text>();
        m_perkinfo = GetComponentInChildren<PerkInfo>(true);
        m_perks.AddRange(m_clip.transform.Find("Perk/slots").GetComponentsInChildren<Button>(true));

        Button button = m_perkinfo.GetComponentInChildren<Button>();
        m_perkinfo.PerkInfoInit();
        button.onClick.AddListener(GetPerk);
        int idx = 0;
        foreach (var x in m_perks)
        {
            int idxx = idx;
            x.onClick.AddListener(() => { m_perkinfo.PopUpInfo(idxx); });
            m_perkImages.Add(x.transform.GetComponent<Image>());
            idx++;
        }
    }

    private void ItemSlotInit()
    {
        m_slots.AddRange(m_clip.transform.Find("Equip/Inventory").GetComponentsInChildren<Button>(true));
        m_iteminfo = GetComponentInChildren<ItemInfo>(true);
        int idx = 0;
        m_iteminfo.ItemInfoInit();
        m_equipunequip = m_iteminfo.transform.GetComponentInChildren<Button>();
        m_equipunequip.onClick.AddListener(EquipUnEquip);
        m_equipbuttontext = m_equipunequip.GetComponentInChildren<Text>();
        foreach (var x in m_slots)
        {
            int idxx = idx;
            x.onClick.AddListener(() => { m_iteminfo.ClickSlot(idxx); });
            m_itemimages.Add(x.transform.Find("Item").GetComponent<Image>());
            Item item = Inventory.instance.GetItem(idx);
            if (item != null)
            {
                m_itemimages[idx].sprite = item.m_sprite;
            }
            idx++;
        }
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
            m_equipmentslots[i].onClick.AddListener(() => { Sibilding(2); m_iteminfo.ClickEquipment(idxx); });
        }
    }

    public void InventoryReset()
    {

        int idx = 0;
        foreach (var x in m_slots)
        {
            Item item = Inventory.instance.GetItem(idx);
            m_itemimages[idx].sprite = SpriteMng.s_blanksprite;
            if (item != null)
            {
                m_itemimages[idx].sprite = item.m_sprite;
            }
            idx++;
        }
    }

    public void PerksReset()
    {
        int idx = 0;
        int point = m_hero.MyStatus.LV - 1;
        if (m_hero.MyStatus.Passive(11))
            point++;

        m_perkpoint.text = m_hero.MyStatus.Perkpoint.ToString() + '/' + ((point)).ToString();
        foreach (var x in m_perks)
        {
            int on = m_hero.MyStatus.Perks(idx) ? 1 : 0;
            m_perkImages[idx].sprite = SpriteMng.s_perkicons[on, idx];
            idx++;
        }
    }
    public void StatusWindowReset()
    {
        m_DSinfostats.text =
            m_hero.MyStatus.LV.ToString() + '\n' +
            string.Format("{0}({1}){2}", m_hero.MyStatus.ORILife, m_hero.MyStatus.MaxLife - m_hero.MyStatus.ORILife, m_hero.MyStatus.Token("LIFE"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.MyStatus.ORIMana, m_hero.MyStatus.Mana - m_hero.MyStatus.ORIMana, m_hero.MyStatus.Token("MANA"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.MyStatus.ORIAD, m_hero.MyStatus.AD - m_hero.MyStatus.ORIAD, m_hero.MyStatus.Token("AD"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.MyStatus.ORIAP, m_hero.MyStatus.AP - m_hero.MyStatus.ORIAP, m_hero.MyStatus.Token("AP"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.MyStatus.ORIDF, m_hero.MyStatus.DF - m_hero.MyStatus.ORIDF, m_hero.MyStatus.Token("DF"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.MyStatus.ORIAS, m_hero.MyStatus.AS - m_hero.MyStatus.ORIAS, m_hero.MyStatus.Token("AS"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.MyStatus.ORILPS, m_hero.MyStatus.LPS - m_hero.MyStatus.ORILPS, m_hero.MyStatus.Token("LPS"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.MyStatus.ORIMPS, m_hero.MyStatus.MPS - m_hero.MyStatus.ORIMPS, m_hero.MyStatus.Token("MPS"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.MyStatus.ORICLI, m_hero.MyStatus.CLI - m_hero.MyStatus.ORICLI, m_hero.MyStatus.Token("CLI"))
            + '\n' +
            string.Format("{0}({1}){2}", m_hero.MyStatus.ORIAVD, m_hero.MyStatus.AVOID - m_hero.MyStatus.ORIAVD, m_hero.MyStatus.Token("AVD"))
            + '\n' +
            m_hero.MyStatus.Range.ToString();

        m_passives.text = "";
        for (int i = 0; i < TableMng.Instance.TableLength(TableType.PassiveTable); i++)
        {
            if(m_hero.MyStatus.Passive(i))
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

    public void PageAddRemove(int val)
    {
        m_rimitidx += val;
    }

    public void PopUp()
    {
        InventoryReset();
        m_iteminfo.ClickSlot(1000);
        PageOpen(m_bookidx);
    }

    public void NextPage()
    {
        m_bookidx = m_bookidx + 1 > m_rimitidx - 1 ? 0 : m_bookidx + 1;
        m_iteminfo.ClickSlot(1000);
        PageOpen(m_bookidx);
    }
    public void PrevPage()
    {
        m_bookidx = m_bookidx - 1 < 0 ? m_rimitidx - 1 : m_bookidx - 1;
        m_iteminfo.ClickSlot(1000);
        PageOpen(m_bookidx);
    }

    public void EquipUnEquip()
    {
        Item item = null;
        if (m_iteminfo.m_focus == 1)
        {
            item = Inventory.instance.GetItem(m_iteminfo.m_targetidx);
            m_iteminfo.m_focus = 2;
            switch (item.m_type)
            {
                case "WEAPON": ItemSwap(0, item); m_iteminfo.m_targetidx = 0; break;
                case "ARMOR": ItemSwap(1, item); m_iteminfo.m_targetidx = 1; break;
                case "TRINKET": ItemSwap(2, item); m_iteminfo.m_targetidx = 2; break;
            }
            m_equipbuttontext.text = "UnEquip";
        }
        else if (m_iteminfo.m_focus == 2)
        {
            item = m_hero.MyStatus.EquipMent[m_iteminfo.m_targetidx];
            Inventory.instance.AddItem(item);
            m_hero.MyStatus.EquipMent[m_iteminfo.m_targetidx] = null;
            m_equipmentimages[m_iteminfo.m_targetidx].sprite = SpriteMng.s_blanksprite;
            m_iteminfo.m_focus = 1;
            m_iteminfo.m_targetidx = Inventory.instance.ItemCount() - 1;
            m_equipbuttontext.text = "Equip";
        }
        m_hero.MyStatus.StatReLoad();
        InventoryReset();
        PageOpen(m_bookidx);

    }

    public void GetPerk()
    {
        m_hero.MyStatus.UsePerkPoint(m_perkinfo.m_targetidx);
        PerksReset();
        PageOpen(m_bookidx);
    }

    public void ItemSwap(int num, Item item)
    {
        if (m_hero.MyStatus.EquipMent[num] == null)
        {
            m_hero.MyStatus.EquipMent[num] = item;
            m_equipmentimages[num].sprite = item.m_sprite;
            Inventory.instance.RemoveItem(m_iteminfo.m_targetidx);
            return;
        }
        Inventory.instance.m_items[m_iteminfo.m_targetidx] = m_hero.MyStatus.EquipMent[num];
        m_itemimages[m_iteminfo.m_targetidx].sprite = m_hero.MyStatus.EquipMent[num].m_sprite;
        m_hero.MyStatus.EquipMent[num] = item;
        m_equipmentimages[num].sprite = item.m_sprite;
        return;
    }

    public void EquipMentSlotReset()
    {
        for (int i = 0; i < 3; i++)
        {
            if (m_hero.MyStatus.EquipMent[i] == null)
                m_equipmentimages[i].sprite = SpriteMng.s_blanksprite;
            else
                m_equipmentimages[i].sprite = m_hero.MyStatus.EquipMent[i].m_sprite;
        }
    }

    public void PageOpen(int idx)
    {
        gameObject.SetActive(true);
        m_perkinfo.gameObject.SetActive(false);

        switch (SceneMng.Instance.GetCurrSceneName())
        {
            case "Lobby":
                m_hero = HeroLobbyMng.FindHero(idx);
                break;
            case "Ingame":
                m_hero = CharMng.Instance.FindHero(idx);
                break;
        }




        m_name.text = m_hero.MyStatus.Name;
        m_lv.text = m_hero.MyStatus.LV.ToString();
        m_exptext.text = string.Format("EXP : {0}/{1}", m_hero.MyStatus.EXP, m_hero.MyStatus.MaxExp);
        m_expimage[1].fillAmount = (float)m_hero.MyStatus.EXP / m_hero.MyStatus.MaxExp;
        m_face.sprite = m_hero.Face;
        switch (m_hero.ClassType)
        {
            case ClassType.Hunter: m_heroclass.sprite = SpriteMng.s_classimage[(int)ClassType.Hunter].sprite; break;
            case ClassType.Knight: m_heroclass.sprite = SpriteMng.s_classimage[(int)ClassType.Knight].sprite; break;
            case ClassType.Mage: m_heroclass.sprite = SpriteMng.s_classimage[(int)ClassType.Mage].sprite; break;
            case ClassType.Warrior: m_heroclass.sprite = SpriteMng.s_classimage[(int)ClassType.Warrior].sprite; break;
        }
        m_stateviewtxts[1].text = string.Format("{0}/{1}", m_hero.MyStatus.Life, m_hero.MyStatus.MaxLife);
        m_stateviewtxts[2].text = string.Format("BASE:{0}", m_hero.MyStatus.Mana);
        m_stateviewtxts[3].text = string.Format("{0}({1}) {2}", m_hero.MyStatus.ORIAD, m_hero.MyStatus.AD - m_hero.MyStatus.ORIAD, m_hero.MyStatus.Token("AD"));
        m_stateviewtxts[4].text = string.Format("{0}({1}) {2}", m_hero.MyStatus.ORIAP, m_hero.MyStatus.AP - m_hero.MyStatus.ORIAP, m_hero.MyStatus.Token("AP"));
        m_stateviewtxts[5].text = string.Format("{0}({1}) {2}", m_hero.MyStatus.ORIDF, m_hero.MyStatus.DF - m_hero.MyStatus.ORIDF, m_hero.MyStatus.Token("DF"));
        m_stateviewtxts[6].text = string.Format("{0}({1:F2})", m_hero.MyStatus.ORIAS, m_hero.MyStatus.AS - m_hero.MyStatus.ORIAS);
        m_page.text = string.Format("{0}/{1}", m_bookidx + 1, m_rimitidx);

        EquipMentSlotReset();
        PerksReset();
        StatusWindowReset();
    }

}
