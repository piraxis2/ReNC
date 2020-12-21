using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum IFF
{
    Friend, Foe
}

public class TailMng : MonoBehaviour
{

    private static TailMng s_tailmng;

    public static TailMng Instance
    {
        get
        {
            if (s_tailmng == null)
            {
                s_tailmng = GameObject.Find("Main/TailMng").GetComponent<TailMng>();
                s_tailmng.Init();
            }

            return s_tailmng;
        }
    }

    private List<Tail> m_friendtails = new List<Tail>();
    private List<Tail> m_foetails = new List<Tail>();
    private List<Tail> m_manatails = new List<Tail>();


    public void Init()
    {
        m_foetails.AddRange(transform.Find("Foe").GetComponentsInChildren<Tail>(true));
        m_friendtails.AddRange(transform.Find("Friend").GetComponentsInChildren<Tail>(true));
        m_manatails.AddRange(transform.Find("ManaTail").GetComponentsInChildren<Tail>(true));

        foreach (var x in m_foetails)
        {
            x.Init();
        }
        foreach (var x in m_friendtails)
        {
            x.Init();
        }
        foreach (var x in m_manatails)
        {
            x.Init();
        }
    }

    public void TailGo(Vector3 target, IFF iff, bool inout)
    {
        List<Tail> tails = new List<Tail>();


        switch(iff)
        {
            case IFF.Foe: tails = m_foetails; break;
            case IFF.Friend: tails = m_friendtails; break;
        }


        foreach (var x in tails)
        {
            if (x.gameObject.activeInHierarchy)
            {
                continue;
            }

            x.gameObject.SetActive(true);
            x.SetTail(target, inout);

            return;
        }

    }


    public void ManaTailGo(Vector3 start, Vector3 target)
    {

        foreach (var x in m_manatails)
        {
            if (x.gameObject.activeInHierarchy)
            {
                continue;
            }

            x.gameObject.SetActive(true);
            x.SetManaTail(start,target);

            return;
        }

    }

    public void TailGo(Vector3 start, Vector3 target, System.Action action)
    {

        foreach (var x in m_manatails)
        {
            if (x.gameObject.activeInHierarchy)
            {
                continue;
            }

            x.gameObject.SetActive(true);
            x.SetTail(start, target, action);

            return;
        }

    }



}
