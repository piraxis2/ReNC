using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMng : MonoBehaviour
{

    private static ItemMng m_itemmng;

    public static ItemMng instance
    {

        get
        {
            if (m_itemmng == null)
                m_itemmng = new ItemMng();
            m_itemmng.Init();

            return m_itemmng;
        }
    }


    private Item[] m_equipment = new Item[3];

    private int[,] m_fusion = new int[8, 8];

    public void EquipmentCheck(Item[] equipment, Item inputitem = null)
    {
        m_equipment = equipment;

        int idxx = -1;
        List<int> idx = new List<int>();
        for (int i = 0; i<3; i++)
        {
            if (m_equipment[i] == null)
                continue;

            if (inputitem == null)
            {
                if (m_equipment[i].m_quality == "Common")
                {
                    idx.Add(i);
                    if (idx.Count >= 2)
                        break;
                }
            }
            else
            {
                if (m_equipment[i].m_quality == "Common")
                {
                    idxx = i;
                    break;
                }
            }

        }

        if (inputitem == null)
        {
            if (idx.Count >= 2)
            {
                int x = idx[0];
                int temp1 = m_equipment[idx[0]].m_idx;
                int temp2 = m_equipment[idx[1]].m_idx;
                m_equipment[idx[0]] = null;
                m_equipment[idx[1]] = null;

                m_equipment[x] = TableMng.Instance.Table(TableType.ITEMTable, m_fusion[temp1, temp2]) as Item;
            }
            return;
        }
      

        if (inputitem.m_quality == "Common")
        {
            if (idxx < 0)
                return;

            int x = idxx;
            int temp = m_equipment[idxx].m_idx;
            m_equipment[idxx] = null;
            m_equipment[x] = TableMng.Instance.Table(TableType.ITEMTable, m_fusion[temp, inputitem.m_idx]) as Item;
        }
    }

    public void Init()
    {
        int idx = 1;
        for (int i = 0;i<8;i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (m_fusion[j, i] == 0)
                {
                    m_fusion[i, j] = idx;
                    m_fusion[j, i] = idx;
                    idx++;
                }
            }
        }
    }

}
