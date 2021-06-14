using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumBot : BaseChar
{
    public override void Init()
    {
        base.Init();
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
        m_attacktype = Attacktype.Howitzer;
    }

    public override PixelFx ProjectileCall()
    {
        return FxMng.Instance.FxCall("BombMine"); 
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Boom");

    }

    public override void AttackAction(Node target)
    {

        SetAttacking(true);

        StartCoroutine(ActionContainer.Instance.IEWideAttack(this, target));
    }

    protected override void StatusSet()
    {

        MyStatus.SetName("GumBot");
        MyStatus.PrioritySet(11);
        MyStatus.SetMaxMana(1);
        MyStatus.SetLife(600);
        MyStatus.SetAS(0.6f);
        base.StatusSet();

    }

}
