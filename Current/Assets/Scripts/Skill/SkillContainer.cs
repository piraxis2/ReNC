using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Skillname
{
    none, ThunderStrike, ArrowPenetrate, Bite, WhirlWind, HealBomb, BlackMagic, ManaSteal, ManaBattery,
    ChainLightning, PolyMorph, TNT, LoyalProtect, HealingWave, ShockWave, PointBlankShot, FanofKnives,
    RapidShot, Incinerate, DeSpellGas, BigFist, Kamehameha, SilverBullet, Hammering, Decimate, BoneBumerang, ChickenBite


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
       
        m_skills.Add(Skillname.ThunderStrike, gameObject.AddComponent<ThunderStrike>());
        m_skills.Add(Skillname.ArrowPenetrate, gameObject.AddComponent<ArrowPenetrate>());
        m_skills.Add(Skillname.Bite, gameObject.AddComponent<Bite>());
        m_skills.Add(Skillname.WhirlWind, gameObject.AddComponent<WhirlWind>());
        m_skills.Add(Skillname.HealBomb, gameObject.AddComponent<HealBomb>());
        m_skills.Add(Skillname.BlackMagic, gameObject.AddComponent<BlackMagic>());
        m_skills.Add(Skillname.ManaBattery, gameObject.AddComponent<ManaBattery>());
        m_skills.Add(Skillname.ChainLightning, gameObject.AddComponent<ChainLightning>());
        m_skills.Add(Skillname.PolyMorph, gameObject.AddComponent<PolyMorph>());
        m_skills.Add(Skillname.TNT, gameObject.AddComponent<TNT>());
        m_skills.Add(Skillname.LoyalProtect, gameObject.AddComponent<LoyalProtect>());
        m_skills.Add(Skillname.HealingWave, gameObject.AddComponent<HealingWave>());
        m_skills.Add(Skillname.ShockWave, gameObject.AddComponent<ShockWave>());
        m_skills.Add(Skillname.PointBlankShot, gameObject.AddComponent<PointBlankShot>());
        m_skills.Add(Skillname.FanofKnives, gameObject.AddComponent<FanOfKnives>());
        m_skills.Add(Skillname.RapidShot, gameObject.AddComponent<RapidShot>());
        m_skills.Add(Skillname.Incinerate, gameObject.AddComponent<Incinerate>());
        m_skills.Add(Skillname.DeSpellGas, gameObject.AddComponent<DeSpellGas>());
        m_skills.Add(Skillname.BigFist, gameObject.AddComponent<BigFist>());
        m_skills.Add(Skillname.Kamehameha, gameObject.AddComponent<Kamehameha>());
        m_skills.Add(Skillname.Hammering, gameObject.AddComponent<Hammering>());
        m_skills.Add(Skillname.Decimate, gameObject.AddComponent<Decimate>());
        m_skills.Add(Skillname.BoneBumerang, gameObject.AddComponent<BoneBumerang>());
        m_skills.Add(Skillname.ChickenBite, gameObject.AddComponent<ChickenBite>());
        m_skills[Skillname.ThunderStrike].Init(m_fxmng);
        m_skills[Skillname.ArrowPenetrate].Init(m_fxmng);
        m_skills[Skillname.Bite].Init(m_fxmng);
        m_skills[Skillname.WhirlWind].Init(m_fxmng);
        m_skills[Skillname.HealBomb].Init(m_fxmng);
        m_skills[Skillname.BlackMagic].Init(m_fxmng);
        m_skills[Skillname.ManaBattery].Init(m_fxmng);
        m_skills[Skillname.ChainLightning].Init(m_fxmng);
        m_skills[Skillname.PolyMorph].Init(m_fxmng);
        m_skills[Skillname.TNT].Init(m_fxmng);
        m_skills[Skillname.LoyalProtect].Init(m_fxmng);
        m_skills[Skillname.HealingWave].Init(m_fxmng);
        m_skills[Skillname.ShockWave].Init(m_fxmng);
        m_skills[Skillname.PointBlankShot].Init(m_fxmng);
        m_skills[Skillname.FanofKnives].Init(m_fxmng);
        m_skills[Skillname.RapidShot].Init(m_fxmng);
        m_skills[Skillname.Incinerate].Init(m_fxmng);
        m_skills[Skillname.DeSpellGas].Init(m_fxmng);
        m_skills[Skillname.BigFist].Init(m_fxmng);
        m_skills[Skillname.Kamehameha].Init(m_fxmng);
        m_skills[Skillname.Hammering].Init(m_fxmng);
        m_skills[Skillname.Decimate].Init(m_fxmng);
        m_skills[Skillname.BoneBumerang].Init(m_fxmng);
        m_skills[Skillname.ChickenBite].Init(m_fxmng);

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

    public Skill FindSkill(string name)
    {
        Skillname skillnaeme = (Skillname)Enum.Parse(typeof(Skillname), name);
        Debug.Log((Skillname)Enum.Parse(typeof(Skillname), name));
        if (m_skills.ContainsKey(skillnaeme))
        { return m_skills[skillnaeme]; }
        return null;
    }



}
