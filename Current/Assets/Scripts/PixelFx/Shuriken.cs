using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private int speed = 1000;

    private int[] m_bonedamage = { 125, 225, 1000 };

    private BaseChar m_caster;


   
    private void Update()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        transform.Rotate(0, 0, (Time.deltaTime) * -speed);
    }

    public void BoneStart(BaseChar caster)
    {
        m_caster = caster; 
    }

    public void BoneEnd()
    {
        m_caster = null;
        gameObject.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {

        BaseChar ch = other.GetComponent<BaseChar>();



        if (ch != null)
        {
            if (ch.FOE != m_caster.FOE)
                ch.MyStatus.DamagedLife(m_bonedamage[m_caster.Star - 1], m_caster, ch.CurrNode, DamageType.Skill);
        }


    }

  

}
