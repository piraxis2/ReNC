using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelFx : MonoBehaviour
{
    private string m_name;
    private int m_ID = -1;
    private int m_count = -1;


    public virtual void Init()
    {

    }

    public int ID
    {
        get { return m_ID; }
    }

    public void SetName(string name)
    {
        m_name = name;
    }

    public void SetCount(int val)
    {
        m_count = val;
    }

    public void SetID(int val)
    {
        m_ID = val;
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void ShutActive()
    {
        if (ID >= 0)
        {
            if (ID >= m_count)
            {
                Destroy(gameObject);
            }
        }
        gameObject.SetActive(false);
    }

    public bool GetActive()
    {
        return gameObject.activeInHierarchy;
    }

}
