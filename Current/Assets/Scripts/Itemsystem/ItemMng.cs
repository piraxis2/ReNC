using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMng : MonoBehaviour
{

    List<Item> m_equipment = new List<Item>();

    int[,] m_fusion = new int[8, 8];

    public void EquipmentCheck(List<Item> items)
    {
        m_equipment = items;

        List<int> idx = new List<int>();
        for (int i = 0; i<3; i++)
        {
            if (m_equipment[i].m_quality == "Common")
            {
                idx.Add(i);
                if (idx.Count >= 2)
                    break;
            }
        }

        if (idx.Count >= 2)
        {
            int x = idx[0];
            m_equipment[idx[0]] = null;
            m_equipment[idx[1]] = null;

            idx[0] = m_equipment[idx[0]].m_idx;
            idx[1] = m_equipment[idx[1]].m_idx;
            m_equipment[x] = TableMng.Instance.Table(TableType.ITEMTable, m_fusion[idx[0], idx[1]]) as Item;
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
