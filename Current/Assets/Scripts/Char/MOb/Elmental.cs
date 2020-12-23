using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elmental : BaseChar
{
    public override void Init()
    {
        base.Init();
        Status.SetName("Elmental");
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
        Status.RangeSet(4);

        Status.PrioritySet(11);
        Status.SetLife(500);
        Status.SetAS(0.6f);
        Status.SetAD(40);
        Status.SetDF(20);
        base.StatusSet();

    }

}
