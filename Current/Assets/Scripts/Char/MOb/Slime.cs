using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : BaseChar
{
    public override void Init()
    {
        base.Init();
        MyStatus.SetName("Slime");
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
        m_skill = Skillname.HealBomb;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("ArrowHit");

    }

    protected override void StatusSet()
    {

        MyStatus.RangeSet(2);
        MyStatus.PrioritySet(11);
        MyStatus.SetLife(500);
        MyStatus.SetAS(0.65f);
        MyStatus.SetMaxMana(1);
        MyStatus.SetShield(100);
        base.StatusSet();


    }
    public override void DestroyThis()
    {
        SkillContainer.Instance.FindSkill(m_skill).Skillshot(this, CurrNode);
        base.DestroyThis();
    }
}
