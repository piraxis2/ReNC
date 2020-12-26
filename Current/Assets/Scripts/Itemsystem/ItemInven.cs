using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInven : Mng
{

    private Item[] m_inventory = new Item[12];
    private List<Image> m_itemicon = new List<Image>();
    private Sprite m_blank;

    public Item[] Inven
    {
        get { return m_inventory; }
    }

    public override void Init()
    {
        m_blank = Resources.Load<Sprite>("SkillIcon/Blank");
        List<ItemDrag> templist = new List<ItemDrag>();
        templist.AddRange(GetComponentsInChildren<ItemDrag>());

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

    public void test()
    {
      
        Item temp = TableMng.Instance.Table(TableType.ITEMTable, 1) as Item;
        ADDItem(temp);
    }


    public void RemoveItem(int idx)
    {
        m_inventory[idx] = null;
        m_itemicon[idx].sprite = m_blank;
    }

}
