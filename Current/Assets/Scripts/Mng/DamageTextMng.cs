using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextMng : MonoBehaviour
{
    private static List<DamageText> m_damagetexts = new List<DamageText>();
    private static DamageText m_damagetext;

    private static DamageTextMng s_damagetextmng;


    public void Init()
    {
        m_damagetext = Resources.Load("Prefab/Damage") as DamageText;
        m_damagetexts.AddRange(GetComponentsInChildren<DamageText>(true));
        int count = 0;
        foreach(var x in m_damagetexts)
        {
            x.SetID(count);
            x.Init();
            count++;
        }
    }

    public static void DamageCall(int dam, BaseChar pos, string text = null)
    {
        foreach(var x in m_damagetexts)
        {
            if (x.Isrun)
                continue;

            x.GetDamage(dam, pos, text);
            return;
        }
        DamageText damageText = Instantiate(m_damagetext);
        m_damagetexts.Add(damageText);
        damageText.GetDamage(dam, pos, text);
    }


}
