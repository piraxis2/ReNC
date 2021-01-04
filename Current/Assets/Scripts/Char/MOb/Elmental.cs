using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elmental : BaseChar
{
    public override void Init()
    {
        base.Init();
        MyStatus.SetName("Elmental");
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 150;
        m_sprite = transform.Find("Core").GetComponent<SpriteRenderer>();
        m_animator = transform.Find("Core").GetComponent<Animator>();
        m_face = m_sprite.sprite;

    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Boom");

    }
    
    public override PixelFx ProjectileCall()
    {
        return FxMng.Instance.FxCall("FireBall");
    }

    protected override void StatusSet()
    {
        MyStatus.RangeSet(4);

        MyStatus.PrioritySet(11);
        MyStatus.SetLife(500);
        MyStatus.SetAS(0.6f);
        MyStatus.SetAD(40);
        MyStatus.SetDF(20);
        base.StatusSet();

    }

}
