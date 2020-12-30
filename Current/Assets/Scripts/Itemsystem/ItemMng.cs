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
    WaitForSeconds second10 = new WaitForSeconds(0.1f);

    public void EquipmentCheck(BaseChar bc, List<Item> surplus)
    {

        Item[] equipment = bc.Status.m_Equipment;
        for (int i = 0; i < 3; i++)
        {
            equipment[i] = null;
        }

        HashSet<Item> toremove = new HashSet<Item>();
        int idx = 0;
        foreach (var x in surplus)
        {
            if (idx < 3)
            {
                if (x.m_quality == "Rare")
                {
                    equipment[idx] = x;
                    toremove.Add(x);
                    idx++;
                }
            }
        }
        surplus.RemoveAll(toremove.Contains);

        bool check = true;
        while (surplus.Count > 0)
        {
            int num = -1;
            for (int i = 0; i < 3; i++)
            {
                if (equipment[i] == null)
                {
                    num = i; break;
                }


                if (i == 2)
                {
                    if (equipment[i].m_quality == "Common")
                    {
                        if (surplus[0].m_quality == "Common")
                        {
                            EquipmentCheck(equipment, surplus[0]);
                            surplus.RemoveAt(0);
                        }
                        else
                        {
                            check = false;
                        }
                    }
                }
            }

            if(num<0)
            {
                check = false;
            }

            else if (num >= 0)
            {
                equipment[num] = surplus[0];
                EquipmentCheck(equipment);
                surplus.RemoveAt(0);
            }

            if (!check)
            {
                break;
            }
        }

  
        if (surplus.Count > 0)
        {
            ItemReTurn(surplus.ToArray(), bc.transform.position);
        }


    }

    private void PushItem(List<Item> surplus)
    {

        int idx = FindEmpty(m_equipment);
        if (idx >= 0)
        {
            m_equipment[idx] = surplus[0];
            surplus.RemoveAt(0);
        }
        else
        {
            m_equipment[2] = TableMng.Instance.Table(TableType.ITEMTable, m_fusion[m_equipment[2].m_idx, surplus[0].m_idx]) as Item;
            surplus.RemoveAt(0);
            return;
        }


        if (surplus.Count == 0)
            return;

        if (InputPassible(surplus[0]))
            PushItem(surplus);
    }

    public bool InputPassible(Item item)
    {

        if (FindEmpty(m_equipment) >= 0)
            return true;

        if (m_equipment[2].m_quality == "Common" && item.m_quality == "Common")
            return true;

        return false;
    }




    public int FindEmpty(object[] arry)
    {
        for (int i = 0; i < arry.Length; i++)
        {
            if (arry[i] == null)
            { return i; }
        }
        return -1 ;

    }

    public void EquipmentCheck(Item[] equipment, Item inputitem = null) //������ ��ǲ�� ������ ����
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


            int temp = m_equipment[idxx].m_idx;
            m_equipment[idxx] = null;
            m_equipment[idxx] = TableMng.Instance.Table(TableType.ITEMTable, m_fusion[temp, inputitem.m_idx]) as Item;
        }

    }


    public void ItemReTurn(Item[] items, Vector3 deathpoint)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                Vector3 folder = FolderMng.Instance.m_buttons[0].transform.position;
                Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(folder.x, folder.y, -Camera.main.transform.position.z));
                TailMng.Instance.TailGo(deathpoint, target, null);
                ItemInven.Instance.ADDItem(items[i]);
            }
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
