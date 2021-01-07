using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Kinetic, Skill, Trap, Heal, None, Dot
}


public class Status
{

    public int IsChanged = 0;


    private string m_name = "AAA";
    private int m_lv = 1;
    private int m_exp = 0;
    private int m_maxexp = 20;
    private int m_life = 100;
    private int m_maxlife = 100;
    private int m_mana = 0;
    private int m_floatmaxmana = 100;
    private int m_basemana = 0;
    private int m_fixedmaxmana = 100;
    private int m_ad = 40;
    private int m_ap = 10;
    private int m_df = 20;
    private int m_range = 1;
    private float m_attackspeed = 0.65f;
    private int m_lps = 1;
    private int m_mps = 1;
    private int m_perkpoint = 0;
    private int m_oderpriority;
    private int m_clitical = 50;
    private int m_avoid = 0;
    private int m_perks = 0;//특성을 비트연산으로 관리
    private int m_passive = 0;
    private int m_movepoint = 1;


    private int m_lv1ad = 0;
    private int m_lv1hp = 0;

    public int m_equlife = 0;
    public int m_equmana = 0;
    public int m_equad = 0;
    public int m_equap = 0;
    public int m_equdf = 0;
    public int m_equlps = 0;
    public int m_equmps = 0;
    public float m_equas = 0;
    public int m_equcli = 0;
    public int m_equavoid = 0;
    public int m_equran = 0;
    public int m_magicregest = 0;


    public bool m_stuned = false;

    private bool m_perk8stack = false;
    private bool m_buff = false;
    private int m_buffidxer = 0;

    private List<Buff> m_bufflist = new List<Buff>();
    public List<PixelFx> m_fxrunninglist = new List<PixelFx>();

    public Item[] m_Equipment = new Item[3];

    private BaseChar m_baseChar;

    public Status(BaseChar baseChar)
    {

        m_baseChar = baseChar;
        Init();
    }

    public void baseset(int ad, int hp)
    {
        m_lv1ad = ad;
        m_lv1hp = hp;
    }

    public void Init()
    {
        m_equad = m_ad;
        m_equap = m_ap;
        m_equdf = m_df;
        m_equlife = m_maxlife;
        m_equmana = m_mana;
        m_equlps = m_lps;
        m_equmps = m_mps;
        m_equas = m_attackspeed;
        m_equcli = m_clitical;
        m_equavoid = m_avoid;
        m_equran = m_range;

    }

    #region //property
    public string Name
    {
        get { return m_name; }
    }

    public int LV
    {
        get { return m_lv; }
    }

    public int EXP
    {
        get { return m_exp; }
    }

    public int ORILife
    {
        get { return m_life; }
    }

    public int Life
    {
        get
        {
            return m_life;
        }
    }

    public int MaxLife
    {
        get
        {
            if (Perks(3))
            {
                return (int)(m_equlife * 1.1f);
            }
            return m_equlife;
        }
    }

    public int ORIMana
    {
        get { return m_mana; }
    }

    public int Mana
    {
        get { return m_mana; }
    }

    public int BaseMana
    {
        get { return m_basemana; }
    }

    public int ORIAD
    {
        get { return m_ad; }
    }

    public int AD
    {
        get
        {
            if (Perks(0))
                return (int)(m_equad * 1.1f);

            return m_equad;
        }
    }

    public int ORIAP
    {
        get { return m_ap; }
    }

    public int AP
    {
        get
        {
            if (Perks(6))
                return (int)(m_equap * 1.1f);

            return m_equap;
        }
    }

    public int ORIDF
    {
        get { return m_df; }
    }

    public int DF
    {
        get
        {
            return m_equdf;
        }
    }

    public int AVOID
    {
        get { return m_equavoid; }
    }

    public int Range
    {
        get { return m_equran; }
    }

    public float ORIAS
    {
        get { return m_attackspeed; }
    }


    public float AS
    {
        get { return m_equas; }
    }

    public int ORILPS
    {
        get { return m_lps; }
    }

    public int LPS
    {
        get { return m_equlps; }
    }

    public int ORIMPS
    {
        get { return m_mps; }
    }

    public int MPS
    {
        get
        {
            if (Perks(7))
                return m_equmps + 5;

            return m_equmps;
        }
    }

    public int CLI
    {
        get
        {
            if (Perks(1))
                return m_equcli + 10;

            return m_equcli;
        }
    }

    public int ORICLI
    {
        get { return m_clitical; }
    }

    public int ORIAVD
    {
        get { return m_avoid; }
    }

    public int Priority
    {
        get { return m_oderpriority; }
    }

    public bool Skillon
    {
        get
        {
            if (m_floatmaxmana == 1)
                return false;

            return (m_mana >= m_floatmaxmana);
        }
    }

    public Item[] EquipMent
    {
        set { m_Equipment = value; }
        get { return m_Equipment; }
    }

    public int Perkpoint
    {
        get { return m_perkpoint; }
    }


    public int MovePoint
    {
        get { return m_movepoint; }
    }

    public int MaxExp
    {
        get { return m_maxexp; }
    }

    public List<Buff> BuffList
    {
        get { return m_bufflist; }
    }


    #endregion

    public void SetLv(int val)
    {
        m_lv = val;
        m_maxexp = val * 20;
    }

    public void SetName(string txt)
    {
        m_name = txt;
    }

    public void SetLife(int val)
    {
        m_maxlife = val;
        m_life = val;
    }

    public void SetAD(int val)
    {
        m_ad = val;
    }

    public void SetAP(int val)
    {
        m_ap = val;
    }

    public void SetDF(int val)
    {
        m_df = val;
    }

    public void SetBaseMana(int val)
    {
        m_basemana = val;
    }

    public void RangeSet(int val)
    {
        m_range = val;
    }

    public void SetAS(float val)
    {
        m_attackspeed = val;
    }

    public void SetMovePoint(int val)
    {
        m_movepoint = val;
    }

    public void SetMaxMana(int val)
    {
        m_floatmaxmana = val;
        m_fixedmaxmana = val;
    }

    public void PrioritySet(int val)
    {
        m_oderpriority = val;
    }

    public void Fusion()
    {
        m_ad = ((int)(m_ad * 1.8f));
        m_maxlife = ((int)(m_maxlife * 1.8f));
        m_life = m_maxlife;
        StatReLoad();
    }



    public bool Perks(int idx)
    {
        return ((1 << idx) & m_perks) == 1 << idx;
    }

    public bool Passive(int idx)
    {
        return ((1 << idx) & m_passive) == 1 << idx;
    }



    public void Upgrade()
    {
        m_maxlife = (int)(m_maxlife * 1.8f);
        m_ad = (int)(m_ad * 1.8f);
        m_life = m_maxlife;
        StatReLoad();
    }

    public float CalculateAttackspeed()
    {
        return 1 / m_equas;
    }

    public int CalculateDamage(int damage)
    {

        int dm = damage;

        float df = ((DF * 100) / (DF + 100));
        df = df / 100;

        dm = (int)(dm -((df*dm)));

        return dm;
    }

    public void DamagedLife(int damage, BaseChar target, Node currnode, DamageType type, string text = null)
    {
        int realdam = damage;
        string damagetext = text;
        string logtext = "";

        if (type == DamageType.Kinetic)
        {
            if (target != null)
            {
                if (Random.Range(1, 100) < (target.MyStatus.CLI))
                {
                    realdam *= 2;
                    damagetext = "CLITICAL";
                    logtext = "크리티컬!!";
                }
            }

            realdam = CalculateDamage(damage);

            if (realdam <= 0)
            {
                realdam = 0;
                damagetext = null;
            }
        }

        if (target != null)
        {
            logtext += target.MyStatus.m_name + " 의 ";
            if (target.MyStatus.Passive(7))
            {
                if (Random.Range(1, 100) < 5)
                {
                    realdam = m_life;
                    damagetext = "DEATHSTRIKE";
                    logtext = "으악! 이건 너무 아프다!" + target.MyStatus.m_name + " 의 죽음의 일격이 " + m_name + " 에게 명중했다.";
                }
            }

        }

        if (Passive(9))
        {
            if (type == DamageType.Skill)
                realdam = (int)(realdam * 0.9f);
        }

        logtext += type.ToString() + " 공격이 " + m_name + " 에게 명중하여 " + realdam.ToString() + " 만큼의 피해를 입혔다.";

        if (type == DamageType.Kinetic)
        {
            if (Random.Range(1, 100) < AVOID)
            {
                realdam = 0;
                damagetext = "MISS";
                logtext = m_name + " 은(는) 날렵하게 회피하여 피해를 입지 않았다!";
            }
        }

        m_life -= realdam;



        Log.Instance.AddText(logtext);
        CanvasMng.Instance.DamageCall(realdam, m_baseChar, damagetext);

        if (m_life <= 0)
        {

            if (currnode.CurrCHAR != null)
            {

                if (!m_baseChar.Dying)
                {
                    m_baseChar.SetDying(true);

                    if (target == null)
                        CharActionMng.ChangeDeadAni(CharActionMng.Direction(m_baseChar.CurrNode, null), m_baseChar);
                    else
                        CharActionMng.ChangeDeadAni(CharActionMng.Direction(m_baseChar.CurrNode, target.CurrNode), m_baseChar);

                    Log.Instance.AddText(m_name + " 이(가) 죽었다....");
                }
            }
        }
    }

    public void CuredLife(int healval, BaseChar target = null)
    {
        int realval = healval * -1;
        if (target != null)
        {
            if (Random.Range(1, 100) < target.MyStatus.CLI)
            {
                realval *= 2;
                m_life -= realval;
                if (m_life > m_maxlife)
                    m_life = m_maxlife;

                Log.Instance.AddText("크리티컬!" + m_name + " 이(가) " + realval.ToString() + " 만큼 회복했다");
                CanvasMng.Instance.DamageCall(realval, m_baseChar, "CLITICAL");
                return;
            }
        }
        m_life -= realval;
        if (m_life > m_maxlife)
            m_life = m_maxlife;

        Log.Instance.AddText(m_name + " 이(가) " + realval.ToString() + " 만큼 회복했다");
        CanvasMng.Instance.DamageCall(realval, m_baseChar);
    }

    public void ExpGet(int exp)
    {
        m_exp += exp;
        if (m_maxexp <= m_exp)
        {
            m_exp -= m_maxexp;
            m_maxexp += 20;
            m_lv++;
            m_perkpoint++;
            ExpGet(0);
        }
    }

    public void ManaGet(int mana)
    {
        if (m_floatmaxmana == 1)
            return;

        m_mana += mana;
        if (m_mana >= m_floatmaxmana)
        {
            m_mana = m_floatmaxmana;
        }
    }

    public void ItemEquip(int idx, Item item)
    {
        m_Equipment[idx] = item;
        StatReLoad();
    }

    public void ManaCost()
    {

        if (Passive(9))
        {
            if (!m_perk8stack)
            {
                m_perk8stack = true;
                return;
            }
        }

        m_perk8stack = false;
        m_mana = 0;
    }

    public void ManaCost(int price)
    {
        m_mana -= price;
        if (m_mana < 0)
            m_mana = 0;
    }

    public void StatReLoad()
    {
        m_equad = (int)ItemCalculation(m_ad, "AD");
        m_equap = (int)ItemCalculation(m_ap, "AP");
        m_equdf = (int)ItemCalculation(m_df, "DF");
        m_equlife = (int)ItemCalculation(m_maxlife, "LIFE");
        m_equmana = (int)ItemCalculation(m_mana, "MANA");
        m_equlps = (int)ItemCalculation(m_lps, "LPS");
        m_equmps = (int)ItemCalculation(m_mps, "MPS");
        m_equas = ItemCalculation(m_attackspeed, "AS");
        m_equcli = (int)ItemCalculation(m_clitical, "CLI");
        m_equavoid = (int)ItemCalculation(m_avoid, "AVD");
        m_magicregest = 0;
        m_equran = m_range;
        m_floatmaxmana = m_fixedmaxmana;

        PassiveCalculation();

        if (m_bufflist.Count > 0)
            BuffCalculation();

        IsChanged++;

    }


    public float ItemCalculation(float oristat, string type)
    {
        float awn = oristat;

        for (int i = 0; i < 3; i++)
        {
            if (EquipMent[i] != null)
            {
                switch (type)
                {
                    case "AD": awn = Item.SplitType(EquipMent[i].m_ad, awn); break;
                    case "AP": awn = Item.SplitType(EquipMent[i].m_ap, awn); break;
                    case "AS": awn = Item.SplitType(EquipMent[i].m_as, awn); break;
                    case "DF": awn = Item.SplitType(EquipMent[i].m_df, awn); break;
                    case "MANA": awn = Item.SplitType(EquipMent[i].m_mana, awn); break;
                    case "LIFE": awn = Item.SplitType(EquipMent[i].m_life, awn); break;
                    case "LPS": awn = Item.SplitType(EquipMent[i].m_liferecovery, awn); break;
                    case "MPS": awn = Item.SplitType(EquipMent[i].m_manarecovery, awn); break;
                    case "CLI": awn = Item.SplitType(EquipMent[i].m_cli, awn); break;
                    case "AVD": awn = Item.SplitType(EquipMent[i].m_avoid, awn); break;
                }
            }
        }
        return awn;
    }

    public void PassiveCalculation()
    {
        System.Action<Status> action;
        for (int i = 0; i < TableMng.Instance.TableLength(TableType.PassiveTable); i++)
        {
            if (Passive(i))
            {
                action = (TableMng.Instance.Table(TableType.PassiveTable, i) as Passive).m_option;
                if (action != null)
                    action(this);
            }
        }
    }

    public void BuffCalculation()
    {
        foreach (var x in m_bufflist)
        {
            switch (x.m_name)
            {
                case "Enhance": m_equad += 10; break;
                case "SlowAS": m_equas *= 0.5f; break;
                case "Silence": m_floatmaxmana = 1; break;
            }

        }

    }


    public void GetBuff(string name, float time, int val = -1, int val2 = -1, float percentage = -1f, bool permanent = false)
    {
        if (m_bufflist.Count == 0)
            m_buffidxer = 0;

        Buff buff = new Buff();
        buff.m_idx = m_buffidxer;
        buff.m_name = name;
        buff.m_time = time;
        buff.m_val = val;
        buff.m_val2 = val2;
        buff.m_percentage = percentage;


        m_bufflist.Add(buff);
        m_buffidxer++;
        if (!permanent)
            m_baseChar.StartCoroutine(IEbuff(buff));
        StatReLoad();
    }


    public int FindBuff(string name)
    {
        foreach (var x in m_bufflist)
        {
            if (x.m_name == name)
                return x.m_idx;
        }
        return -1;
    }


    public int FindBuff(int idx)
    {
        int num = 0;
        foreach (var x in m_bufflist)
        {
            if (idx == x.m_idx)
                return num;

            num++;
        }
        return -1;
    }

    public IEnumerator IEbuff(Buff buff)
    {

        float bleedingcount = 0.5f;
        bool bleeding = false;
        bool stop = false;
        float elapsedtime = 0;
        float time = buff.m_time;
        int dotdmg = buff.m_val;
        PixelFx fx = buff.Bufffx();

        if (buff.m_name == "Bleeding")
        {
            dotdmg = (dotdmg) / ((int)time * 2);
            bleeding = true;
        }
        else if (buff.m_name == "Stun")
        {
            m_stuned = true;
        }

        if (fx != null)
        {
            fx.gameObject.SetActive(true);
            fx.transform.position = m_baseChar.m_starani.transform.position;
        }

        m_fxrunninglist.Add(fx);

        while (!stop)
        {
            elapsedtime += Time.deltaTime;
            if (fx != null)
                fx.transform.position = m_baseChar.m_starani.transform.position;
            if (bleeding)
            {
                fx.transform.position = m_baseChar.transform.position;
                if(elapsedtime>=bleedingcount)
                {
                    DamagedLife(dotdmg, null, m_baseChar.CurrNode, DamageType.Skill, "BLEEDING");
                    bleedingcount += 0.5f;
                }
            }

            if (elapsedtime >= buff.m_time)
            {
                stop = true;
                elapsedtime = 0;
                buff.Buffoff(this);
                StatReLoad();
                if (fx != null)
                    fx.ShutActive();
            }

            yield return null;
        }
    }

    public char Token(string type)
    {
        char token = '-';
        switch (type)
        {
            case "AD": token = TokenCategorization(m_ad, AD); break;
            case "AP": token = TokenCategorization(m_ap, AP); break;
            case "AS": token = TokenCategorization(m_attackspeed, AS); break;
            case "DF": token = TokenCategorization(m_df, DF); break;
            case "MANA": token = TokenCategorization(m_mana, Mana); break;
            case "LIFE": token = TokenCategorization(m_maxlife, MaxLife); break;
            case "LPS": token = TokenCategorization(m_lps, LPS); break;
            case "MPS": token = TokenCategorization(m_mps, MPS); break;
            case "CLI": token = TokenCategorization(m_clitical, CLI); break;
            case "AVD": token = TokenCategorization(m_avoid, AVOID); break;
        }
        return token;
    }

    public void UsePerkPoint(int idx)
    {
        if (m_perkpoint < 1)
        {
            return;
        }


        if (idx > 8)
            return;

        if (idx % 3 == 0)
        {
            if (!(Perks(idx)))
            {
                m_perks += (1 << idx);
                m_perkpoint--;
            }
        }

        else
        {
            if (Perks(idx - 1))
            {
                if (!Perks(idx))
                {
                    m_perks += (1 << idx);
                    m_perkpoint--;
                    switch (idx)
                    {
                        case 2: GetPassive(7); break;
                        case 5: GetPassive(8); break;
                        case 8: GetPassive(9); break;
                    }
                }
            }
        }
    }

    public void GetPassive(int idx)
    {
        if (TableMng.Instance.TableLength(TableType.PassiveTable) - 1 < idx)
            return;

        if (!Passive(idx))
        {
            m_passive += (1 << idx);
            if (idx == 11)//genius
                m_perkpoint++;
        }
        StatReLoad();
    }

    private char TokenCategorization(float ori, float item)
    {
        if (ori < item)
            return '▲';

        else if (ori > item)
            return '▼';

        return '-';
    }



}
