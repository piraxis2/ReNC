using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TableType
{
    ITEMTable, ShopTable, PERKTable, PassiveTable, SkillInfoTable, DungeonTable, PlayerSkillTable, BuildTable, BuffTable
}


public class TableMng
{
    private Dictionary<TableType, ReadData> m_tableDic
         = new Dictionary<TableType, ReadData>();

    private static TableMng m_instance;

    public static TableMng Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new TableMng();
            }


            return m_instance;
        }
    }

    private TableMng()
    {

    }

    public void AddTable<T>(TableType t) where T : ReadData, new()
    {
        if (!m_tableDic.ContainsKey(t))
        {
            T table = new T();
            table.Load(t.ToString());
            m_tableDic.Add(t, table);
        }
    }
    public int TableLength(TableType table)
    {
        return m_tableDic[table].DicCount();
    }

    public System.Object Table(TableType tType, int idx)
    {
        if (m_tableDic.ContainsKey(tType))
        {
            return m_tableDic[tType].GetInfo(idx);
        }
        return null;
    }


}
