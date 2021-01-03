using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Skillname
{
    none,thunderstrike,arrowpenetrate,Bite
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
        m_skills[Skillname.thunderstrike].Init(m_fxmng);
        m_skills[Skillname.arrowpenetrate].Init(m_fxmng);
        m_skills[Skillname.Bite].Init(m_fxmng);
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
