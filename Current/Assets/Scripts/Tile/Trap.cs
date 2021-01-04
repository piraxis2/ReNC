using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private bool m_Used = false;
    private Animator m_ani;
    private BaseChar m_currChar;
    private BaseChar m_prevChar;

    private void Awake()
    {
        m_ani = GetComponent<Animator>();
    }

    public bool Used
    { get { return m_Used; } }

    public virtual void ActiveTrap(BaseChar trapedchar)
    {
        m_prevChar = m_currChar;
        m_currChar = trapedchar;

        if (m_currChar != m_prevChar)
        {
            m_ani.SetTrigger("ON");
            m_Used = true;
            m_currChar.MyStatus.DamagedLife(100, null, trapedchar.CurrNode, DamageType.Trap, "Trapped");
        }
    }
}
