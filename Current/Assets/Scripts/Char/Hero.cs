using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hero : BaseChar 
{
 
   

    public enum Room
    {
        Lobby,Squad
    }

    private int m_herocardID;
    public Room m_myroom = Room.Lobby;

    public int HeroCardID
    {
        get { return m_herocardID; }
    }


    public override void Init()
    {
        base.Init();
        SetTargetLayer(10);
        SetRace(Race.Hero);
    }

    public void CardSet(int idx)
    {
        m_herocardID = idx;
    }


    protected override void StatusSet()
    {
        

    }


    //public override List<BaseChar> RangeCall()
    //{
    //    List<BaseChar> attackable = new List<BaseChar>();

    //    foreach (var x in m_Rangelist)
    //    {
    //        if (x.CurrCHAR != null)
    //        {
    //            if (x.CurrCHAR.gameObject.layer == 10)
    //            {
    //                attackable.Add(x.CurrCHAR);
    //            }
    //        }
    //    }

    //    return attackable;
    //}

    //public override void DestroyThis()
    //{



    //    base.DestroyThis();
    //    CharMng.Instance.CurrHeros.RemoveAt(CharMng.Instance.FindHeroID(UniqueID));
    //    CharMng.Instance.TotalHeros.RemoveAt(CharMng.Instance.FindID(UniqueID));
    //    if (!m_status.Passive(8))
    //        GameObject.Destroy(gameObject);
    //    else
    //        gameObject.SetActive(false);

    //}

}
