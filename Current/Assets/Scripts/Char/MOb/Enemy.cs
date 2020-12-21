using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseChar 
{
       // Start is called before the first frame update
    public override void Init()
    {
        base.Init();
        SetTargetLayer(9);
    }


    //public override List<BaseChar> RangeCall()
    //{
    //    List<BaseChar> attackable = new List<BaseChar>();

    //    foreach (var x in m_Rangelist)
    //    {
    //        if (x.CurrCHAR != null)
    //        {
    //            if(FOE)
    //                if(!x.CurrCHAR.FOE)
    //                { attackable.Add(x.CurrCHAR); }
    //            else
    //            if(x.CurrCHAR.FOE)
    //                { attackable.Add(x.CurrCHAR); }

    //            //if (x.CurrCHAR.gameObject.layer == 9)
    //            //{
    //            //    attackable.Add(x.CurrCHAR);
    //            //}
    //        }
    //    }

    //    return attackable;
    //}


    //public override void DestroyThis()
    //{


    //    int manapos = SummonerSkillMng.Instance.Mana / 2;
    //    if (manapos < 5)
    //        TailMng.Instance.ManaTailGo(transform.position, SummonerSkillMng.Instance.ManaPositon[manapos]);
    //    if(FactoryID>=m_count)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    base.DestroyThis();
    //    CharMng.Instance.CurrEnemys.RemoveAt(CharMng.Instance.FindEnemyID(UniqueID));
    //    CharMng.Instance.TotalHeros.RemoveAt(CharMng.Instance.FindID(UniqueID));
    //    transform.SetParent(MobPooling.Instance.transform.Find(GetComponent<Enemy>().GetType().Name));
    //    transform.localPosition = Vector3.zero;
    //    m_status.SetLife(m_status.MaxLife);
    //    gameObject.SetActive(false);

    //}

}
