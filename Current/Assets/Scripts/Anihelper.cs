using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anihelper : MonoBehaviour
{
    private Animator m_animator;
    private BaseChar m_char;

    public void Awake()
    {
        m_animator = transform.Find("face").GetComponent<Animator>();
        m_char = transform.parent.GetComponent<BaseChar>();
    }


    public void FaceAni(string name)
    {


        if(name=="NO")
        {
            m_animator.gameObject.SetActive(false);
            return;
        }

        m_animator.gameObject.SetActive(true);

        m_animator.SetTrigger(name);
    }


    public void Dead()
    {
        m_char.DestroyThis();
    }


}
