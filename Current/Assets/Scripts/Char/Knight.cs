using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Hero
{
    public static int[] m_manasteal = { 25, 50, 75 };

    public override void Init()
    {
        base.Init();
        MyStatus.SetName("Knight");
        m_classtype = ClassType.Knight;
        m_classname = "Knight";
        m_hitfx = Resources.Load("Prefab/Sting") as GameObject;
        m_projectileangle = 180;
        m_skill = Skillname.ManaSteal;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Hit");

    }

    protected override void StatusSet()
    {

        MyStatus.SetMaxMana(1);
        MyStatus.SetLife(700);
        MyStatus.RangeSet(1);
        MyStatus.PrioritySet(10);
        MyStatus.SetAS(0.6f);
        base.StatusSet();

    }
}
