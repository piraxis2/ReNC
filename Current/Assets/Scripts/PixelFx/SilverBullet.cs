using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverBullet : PixelFx
{

    private Animator m_ani;
    private Status m_currstat;



    public override void Init()
    {

        m_ani = GetComponent<Animator>();

    }

    private void Update()
    {
        if (m_currstat != null)
            transform.position = m_currstat.m_baseChar.transform.position;
    }

    public void Bind(Status status)
    {
        m_currstat = status;
        gameObject.SetActive(true);
        transform.position = m_currstat.m_baseChar.transform.position;

    }

    public void Count(int x)
    {
        Debug.Log(x);
        m_ani.SetInteger("Bullet", x);

    }

    public void Anioff()
    {
        m_currstat.m_onhitstack = 0;
        m_currstat = null;
        m_ani.SetInteger("Bullet", 0);
        gameObject.SetActive(false);
    }


}
