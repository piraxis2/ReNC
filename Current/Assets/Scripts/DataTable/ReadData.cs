using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData  
{
    protected Dictionary<int, System.Object> m_infoDic = new Dictionary<int, System.Object>();

    public ReadData()
    {

    }

    protected void AddInfo(int idx, System.Object val)
    {
        if (!m_infoDic.ContainsKey(idx))
            m_infoDic.Add(idx, val);
    }

    protected virtual void Parse(string text)
    {

    }

    public void Load(string path)
    {
        TextAsset t = Resources.Load<TextAsset>(path);
        if (t != null)
            Parse(t.text);

    }

    public int DicCount()
    {
        return m_infoDic.Count;
    }

    public System.Object GetInfo(int idx)
    {
        if (m_infoDic.ContainsKey(idx))
        {
            return m_infoDic[idx];
        }
        return null;
    }
}
