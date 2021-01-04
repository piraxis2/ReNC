using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    private Image m_iteminfoicon;
    private Text m_itemname;
    private Text m_infonums;
    private Text m_infonames;
    private Text m_infoloer;
    private Text m_passivetext;
    private Text m_price;
    private Transform m_equipbutton;
    private Text m_buttontext;

    public int m_targetidx;
    [HideInInspector]
    public int m_focus = -1;

    public void ItemInfoInit()
    {
        m_itemname = transform.Find("Name").GetComponent<Text>();
        m_iteminfoicon = transform.Find("IconTray/Icon").GetComponent<Image>();
        m_infonums = transform.Find("ItemState/State").GetComponent<Text>();
        m_infonames = transform.Find("ItemState/InfoName").GetComponent<Text>();
        m_infoloer = transform.Find("ItemState/Loer").GetComponent<Text>();
        m_price = transform.Find("Price").GetComponent<Text>();
        m_passivetext = transform.Find("ItemState/Passive/Passive").GetComponent<Text>();
        gameObject.SetActive(false);
        m_equipbutton = transform.Find("EquipButton");
        if (m_equipbutton != null)
        {
            m_buttontext = m_equipbutton.transform.GetComponentInChildren<Text>();
        }
    }

    public void ClickSlot(int idx)
    {
        m_focus = 1;
        Item item = Inventory.instance.GetItem(idx);
        m_targetidx = idx;
        if (m_buttontext != null)
            m_buttontext.text = "Equip";
        PopInfo(item);
        
    }

    public void ClickShop(int idx)
    {
        m_focus = 0;
        m_targetidx = idx;
        List<int> stufflist = (TableMng.Instance.Table(TableType.ShopTable, GameData.Instance.m_globalturn)) as List<int>;
        Item item = null;
        if (stufflist.Count > idx)
            item = TableMng.Instance.Table(TableType.ITEMTable, stufflist[idx]) as Item;

        PopInfo(item);
    }

    public void ClickEquipment(int idx)
    {
        m_focus = 2;
        m_targetidx = idx;
        Item item = HeroLobbyMng.FindHero(CharacterBook.Instance.m_bookidx).MyStatus.EquipMent[idx];
        if (m_buttontext != null)
            m_buttontext.text = "UnEquip";
        PopInfo(item);
    }


    private void PopInfo(Item item)
    {
        if (item == null)
        {
            m_focus = -1;
            m_targetidx = -1;
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);

        m_infonames.text = string.Empty;
        m_infonums.text = string.Empty;
        m_iteminfoicon.sprite = item.m_sprite;

        m_itemname.text = NameReduce(item.m_name);
        m_infonames.text += "Type   :\n";
        m_infonums.text += item.m_type + '\n';
        m_infonames.text += "Quality:\n";
        m_infonums.text += item.m_quality + '\n';
        StateClassify(item);
        m_infoloer.text = '"' + (item.m_loer) + '"';
        if (m_focus == 1)
        {
            m_price.text = ((int)item.m_price * 0.3f).ToString();
            return;
        }

        m_price.text = item.m_price.ToString();
    }

    public string Type(string text)
    {
        switch (text[0])
        {
            case 'p': return Item.SplitString(text).ToString() + "+";
            case 'm': return Item.SplitString(text).ToString() + "%";
            case 'd': return Item.SplitString(text).ToString() + "-";
        }
        return string.Empty ;
    }

    public void StateClassify(Item item)
    {

        if (item.m_ad[0] != 'n')
        {
            m_infonames.text += "AD     :\n";
            m_infonums.text += Type(item.m_ad)+ "\n";
        }
        if (item.m_ap[0] != 'n')
        {
            m_infonames.text += "AP     :\n";
            m_infonums.text += Type(item.m_ap) + "\n";
        }
        if (item.m_as[0] != 'n')
        {
            m_infonames.text += "AS     :\n";
            m_infonums.text += Type(item.m_as) + "\n";
        }
        if (item.m_df[0] != 'n')
        {
            m_infonames.text += "DF     :\n";
            m_infonums.text += Type(item.m_df) + "\n";
        }
        if (item.m_mana[0] != 'n')
        {
            m_infonames.text += "MANA   :\n";
            m_infonums.text += Type(item.m_mana) + "\n";
        }
        if (item.m_life[0] != 'n')
        {
            m_infonames.text += "LIFE   :\n";
            m_infonums.text += Type(item.m_life) + "\n";
        }
        if (item.m_liferecovery[0] != 'n')
        {
            m_infonames.text += "LPS    :\n";
            m_infonums.text += Type(item.m_liferecovery) + "\n";
        }
        if (item.m_manarecovery[0] != 'n')
        {
            m_infonames.text += "MPS    :\n";
            m_infonums.text += Type(item.m_manarecovery) + "\n";
        }

    }

    private string NameReduce(string name)
    {
        char[] reducename = name.ToCharArray();

        if (name.Length > 13)
        {
            reducename = ".............".ToCharArray();
            for (int i = 0; i < 10; i++)
            {
                reducename[i] = name.ToCharArray()[i];
            }
        }

        return new string(reducename);
    }

}
