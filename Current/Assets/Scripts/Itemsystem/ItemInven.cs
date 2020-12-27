using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInven : MonoBehaviour
{

    private static ItemInven m_iteminven;
    public static ItemInven Instance
    {
        get
        {
            if (m_iteminven == null)
            {
                m_iteminven = FindObjectOfType<ItemInven>();
                m_iteminven.Init();
            }
            return m_iteminven;
        }
    }

    private Item[] m_inventory = new Item[12];
    private List<Image> m_itemicon = new List<Image>();
    private Sprite m_blank;

    public Item[] Inven
    {
        get { return m_inventory; }
    }

    public void Init()
    {
        m_blank = Resources.Load<Sprite>("SkillIcon/Blank");
        List<ItemDrag> templist = new List<ItemDrag>();
        templist.AddRange(FolderMng.Instance.gameObject.GetComponentsInChildren<ItemDrag>());

        int idx = 0;
        foreach (var x in templist)
        {
            m_itemicon.Add(x.transform.GetChild(0).GetComponent<Image>());
            x.Init(idx,this);
            idx++;
        }
    }

    private int SearchEmpty()
    {
        for (int i = 0; i < 12; i++)
        {
            if (m_inventory[i] == null)
                return i;
        }
        return -1;
    }

    public void ADDItem(Item item)
    {

        int idx = SearchEmpty();

        if (idx < 0)
            return;

        m_inventory[idx] = item;
        m_itemicon[idx].sprite = item.m_sprite;
    }

    public bool ADDItem(Item item,int idx)
    {
        if (m_inventory[idx] != null)
            return false;

        if (item == null)
            return false;

        m_inventory[idx] = item;
        m_itemicon[idx].sprite = item.m_sprite;
        return true;
    }

    public void test()
    {
        int idx = Random.Range(1, 8);


        Item temp = TableMng.Instance.Table(TableType.ITEMTable, idx) as Item;
        ItemInven.Instance.ADDItem(temp);
    }


    public void RemoveItem(int idx)
    {
        m_inventory[idx] = null;
        m_itemicon[idx].sprite = m_blank;
    }

}
