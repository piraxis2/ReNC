using System.Collections.Generic;
using UnityEngine;

public class MobPooling : MonoBehaviour
{
    static MobPooling s_mobpooling;

    public static MobPooling Instance
    {

        get
        {
            if (s_mobpooling == null)
            {
                s_mobpooling = GameObject.Find("Main/MobPooling").GetComponent<MobPooling>();
                s_mobpooling.Init();
            }
            return s_mobpooling;

        }
    }



    private List<List<BaseChar>>[] m_article;
    private List<List<BaseChar>> m_mobs = new List<List<BaseChar>>();
    private List<GameObject> m_prefabs = new List<GameObject>();

    private int[] m_tiercout= { 9, 9, 9, 6, 4 };

    public void Init()
    {
        m_prefabs.Add(Resources.Load("Prefab/Character/Sans") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/Character/slime") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/Character/Goblin") as GameObject);

        for (int i = 0; i < m_prefabs.Count; i++)
        {

            m_mobs.Add(new List<BaseChar>());
            m_mobs[i].AddRange(s_mobpooling.transform.GetChild(i).GetComponentsInChildren<BaseChar>(true));
            ListIndexer(m_mobs[i]);
        }
    }

    void ListIndexer(List<BaseChar> list)
    {
        int index = 0;
        foreach (var x in list)
        {
            x.SetFactoryID(index);
            x.SetCount(list.Count);
            index++;
        }

    }

    public BaseChar CharCall(int row, int col)
    {
        return MobInstant(row, col);
    }
    public BaseChar CharCall(string name)
    {
        switch (name)
        {
            case "Sans":
                return MobInstant(0);
            case "Slime":
                return MobInstant(1);
            case "Goblin":
                return MobInstant(2);
        }
        return null;
    }


    public BaseChar CharCall(int idx)
    {
        
        if (m_mobs.Count > idx)
        {
            return MobInstant(idx);
        }
        return null;
    }


    private BaseChar MobInstant(int index)
    {
        List<BaseChar> mobs = m_mobs[index];
        foreach (var x in mobs)
        {
            if (x.gameObject.activeInHierarchy)
                continue;

            return x;
        }
        BaseChar ch = Instantiate(m_prefabs[index], s_mobpooling.transform.GetChild(index).transform).GetComponent<BaseChar>();
        ch.SetFactoryID(mobs.Count);
        return ch;


    }
    private BaseChar MobInstant(int row,int col)
    {
        List<BaseChar> mobs = m_article[row][col];
        foreach (var x in mobs)
        {
            if (x.gameObject.activeInHierarchy)
                continue;

            return x;
        }
        BaseChar ch = Instantiate(m_prefabs[row], s_mobpooling.transform.GetChild(row).GetChild(col).transform).GetComponent<BaseChar>();

        return null;
    }

}
