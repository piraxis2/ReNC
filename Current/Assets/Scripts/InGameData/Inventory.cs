using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> m_items = new List<Item>();

    private static Inventory s_inventory;

    public static Inventory instance
    {
        get
        {
            if (s_inventory == null)
            {
                s_inventory = new Inventory();
            }
            return s_inventory;
        }
    }

    Inventory()
    {

    }


    public int ItemCount()
    {
        return m_items.Count;
    }

    public void AddItem(Item itm)
    {
        m_items.Add(itm);
    }
    public void RemoveItem(int idx)
    {
        if (m_items[idx] != null)
            m_items.RemoveAt(idx);
    }

    public Item GetItem(int idx)
    {
        if (idx < m_items.Count)
            return m_items[idx];

        return null;
    }

}
