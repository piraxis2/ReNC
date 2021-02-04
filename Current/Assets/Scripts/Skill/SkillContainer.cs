using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Skillname
{
    none, thunderstrike, arrowpenetrate, Bite, WhirlWind, HealBomb, BlackMagic, ManaSteal, ManaBattery, ChainLightning, PolyMorph, TNT, LoyalProtect, HealingWave, ShockWave

}

public class SkillContainer : MonoBehaviour 
{
    private Dictionary<Skillname,Skill> m_skills = new Dictionary<Skillname,Skill>();
    private FxMng m_fxmng;
    public static SkillContainer s_skillcontainer;




    public void init()
    {
        s_skillcontainer = this;
        m_fxmng = GetComponent<FxMng>();
       
        m_skills.Add(Skillname.thunderstrike, gameObject.AddComponent<ThunderStrike>());
        m_skills.Add(Skillname.arrowpenetrate, gameObject.AddComponent<ArrowPenetrate>());
        m_skills.Add(Skillname.Bite, gameObject.AddComponent<Bite>());
        m_skills.Add(Skillname.WhirlWind, gameObject.AddComponent<WhirlWind>());
        m_skills.Add(Skillname.HealBomb, gameObject.AddComponent<HealBomb>());
        m_skills.Add(Skillname.BlackMagic, gameObject.AddComponent<BlackMagic>());
        m_skills.Add(Skillname.ManaBattery, gameObject.AddComponent<ManaBattery>());
        m_skills.Add(Skillname.ChainLightning, gameObject.AddComponent<ChainLightning>());
        m_skills.Add(Skillname.PolyMorph, gameObject.AddComponent<PolyMorph>());
        m_skills.Add(Skillname.TNT, gameObject.AddComponent<TNT>());
        m_skills.Add(Skillname.LoyalProtect, gameObject.AddComponent<LoyalProtect>());
        m_skills[Skillname.thunderstrike].Init(m_fxmng);
        m_skills[Skillname.arrowpenetrate].Init(m_fxmng);
        m_skills[Skillname.Bite].Init(m_fxmng);
        m_skills[Skillname.WhirlWind].Init(m_fxmng);
        m_skills[Skillname.HealBomb].Init(m_fxmng);
        m_skills[Skillname.BlackMagic].Init(m_fxmng);
        m_skills[Skillname.ManaBattery].Init(m_fxmng);
        m_skills[Skillname.ChainLightning].Init(m_fxmng);
        m_skills[Skillname.PolyMorph].Init(m_fxmng);
        m_skills[Skillname.TNT].Init(m_fxmng);
        m_skills[Skillname.LoyalProtect].Init(m_fxmng);

    }

    public static SkillContainer Instance
    {
        get
        {
            return s_skillcontainer;
        }
    }

    public Skill FindSkill(Skillname skill)
    {
        if (m_skills.ContainsKey(skill))
        { return m_skills[skill]; }

        return null;
    }

    public Skill FindSkill(int idx)
    {
        if(m_skills.ContainsKey((Skillname)idx))
        { return m_skills[(Skillname)idx]; }
        return null;
    }

    public Skill FindSkill(string name)
    {
        Skillname skillnaeme = (Skillname)Enum.Parse(typeof(Skillname), name);
        if (m_skills.ContainsKey(skillnaeme))
        { return m_skills[skillnaeme]; }
        return null;
    }



}
