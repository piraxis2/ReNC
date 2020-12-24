using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIR
{
    EAST, WEST, SOUTH, NORTH
}



public class CharActionMng : Mng
{

    private PathFind m_pathfinder;
    private PassengerComparer m_passengerComparer = new PassengerComparer();
    private CanvasMng m_canvasmng;
    private BaseChar m_CurrChar;

    private List<Node> m_StartNodes = new List<Node>();

    [SerializeField]
    private float m_speed = 1;
    private int m_globalturn = 0;


    public int Turn
    {
        get { return m_globalturn; }
    }
    int sx = 0;

    public override void Init()
    {
        m_pathfinder = transform.parent.GetComponentInChildren<PathFind>();
    }



    private void Routine()
    {
        m_CurrChar = null;
        m_globalturn++;
        CanvasMng.Instance.SetTurn(m_globalturn);
        CharMng.Instance.TotalHeros.Sort(m_passengerComparer);


        for (int i = 0; i < CharMng.Instance.TotalHeros.Count; i++)
        {
            if (CharMng.Instance.TotalHeros[i] == null)
                continue;

            m_CurrChar = CharMng.Instance.TotalHeros[i];
            List<BaseChar> targets = m_CurrChar.RangeCall();
            targets.Sort(m_passengerComparer);
            targets.Reverse();

            if ((targets.Count == 0))
            {
                m_CurrChar.SetAttacking(false);

                if (m_CurrChar.Status.MovePoint > 0)
                {
                    if (m_CurrChar.FOE)
                        ShortestMove(m_CurrChar, CharMng.Instance.CurrEnemys);
                    else
                        ShortestMove(m_CurrChar, CharMng.Instance.CurrHeros);
                }
            }
            else
            {
                if (!m_CurrChar.Attacking)
                {

                    if (m_CurrChar.Status.Skillon)
                    {
                        if (m_CurrChar.Skill != Skillname.none)
                        {
                            m_CurrChar.SetAttacking(true);
                            ChangeSkillAni(Direction(m_CurrChar.CurrNode, targets[0].CurrNode), m_CurrChar);
                            SkillContainer.Instance.FindSkill(m_CurrChar.Skill).Skillshot(m_CurrChar, targets[0].CurrNode);
                            m_CurrChar.Status.ManaCost();
                        }
                        else// 스킬없는캐릭터는 스킬쓰지않고 공격
                        {
                            m_CurrChar.SetAttacking(true);
                            if (m_CurrChar.Status.Range == 1)
                                 StartCoroutine(IEAttack(m_CurrChar, targets[0].CurrNode));
                            else
                                StartCoroutine(IELongRange(m_CurrChar, targets[0].CurrNode));
                        }
                    }
                    else
                    {
                        m_CurrChar.SetAttacking(true);
                        if (m_CurrChar.Status.Range == 1)
                            StartCoroutine(IEAttack(m_CurrChar, targets[0].CurrNode));
                        else
                            StartCoroutine(IELongRange(m_CurrChar, targets[0].CurrNode));
                    }
                }
            }
        }
    }

    private void ShortestMove(BaseChar Chara,List<BaseChar> targetlist)
    {
        List<List<Node>> pathlist = new List<List<Node>>();
        List<Node> ownpath;
        for (int i = 0; i < targetlist.Count; i++)
        {
            pathlist.Add(m_pathfinder.FindPath(Chara.CurrNode, targetlist[i].CurrNode));
        }

        ownpath = pathlist[0];

        for (int i = 1; i < pathlist.Count; i++)
        {
            if (pathlist[i].Count == 0)
                continue;

            if (ownpath.Count > pathlist[i].Count)
            {
                ownpath = null;
                ownpath = pathlist[i];
            }
        }

        if (ownpath.Count > 0)
            StartCoroutine(IEMove(Chara, ownpath[1]));
    }

   
    public IEnumerator IEAttack(BaseChar Chara, Node target)
    {

        float AS = Chara.Status.CalculateAttackspeed();
        WaitForSecondsRealtime attackspeed = new WaitForSecondsRealtime(AS);

        float angle = 0;
        while (IsExist(Chara, target))
        {
            

            if (Chara.Status.Life <= 0)
                yield break;

            if(AS!=Chara.Status.CalculateAttackspeed())
            {
                AS = Chara.Status.CalculateAttackspeed();
                attackspeed = new WaitForSecondsRealtime(AS);
            }

        
            ChangeAttackAni(Direction(Chara.CurrNode, target), Chara);
            angle = MathHelper.GetAngle(Chara.transform.position, target.transform.position) + Chara.m_projectileangle;
            PixelFx Hit = Chara.FxCall();
            Hit.gameObject.SetActive(true);
            Hit.transform.position = target.CurrCHAR.transform.position + new Vector3(0, 0.5f, 0);
            target.CurrCHAR.Status.DamagedLife(Chara.Status.AD, Chara, target,DamageType.Kinetic);
            Chara.Status.ManaGet(5+Chara.Status.MPS);
            yield return attackspeed;

            if (Chara.Status.Skillon)
            {
                Chara.SetAttacking(false);
                yield break;
            }

        }
        Chara.SetAttacking(false);
        yield return null;
    }

    public IEnumerator IELongRange(BaseChar Chara, Node target)
    {
        float AS = Chara.Status.CalculateAttackspeed();
        WaitForSecondsRealtime attackspeed = new WaitForSecondsRealtime(AS);


        while (IsExist(Chara, target))
        {
            if (Chara.Status.Life <= 0)
                yield break;

            if (AS != Chara.Status.CalculateAttackspeed())
            {
                AS = Chara.Status.CalculateAttackspeed();
                attackspeed = new WaitForSecondsRealtime(AS);
            }



            ChangeAttackAni(Direction(Chara.CurrNode, target), Chara);
            StartCoroutine(IEProjectile(Chara, target));

            yield return attackspeed;
            if (Chara.Status.Skillon)
            {
                Chara.SetAttacking(false);
                yield break;
            }

        }
        Chara.SetAttacking(false);
        yield return null;

    }

    public IEnumerator IEProjectile(BaseChar Chara, Node target)
    {
        if (Chara == null || target.CurrCHAR == null)
            yield break;

        BaseChar targetChar = target.CurrCHAR;
        Vector3 pos = Chara.transform.position + new Vector3(0, 0.5f, 0);
        Vector3 enemypos = targetChar.transform.position + new Vector3(0, 0.5f, 0);

        PixelFx projectile = Chara.ProjectileCall();
        projectile.gameObject.SetActive(true);

        int angle = Chara.m_projectileangle;

        float elapsedtime = 0;
        bool stop = false;

        while (!stop)
        {
            elapsedtime += Time.deltaTime * 8f;
            elapsedtime = Mathf.Clamp01(elapsedtime);


            projectile.transform.position = Vector3.Lerp(pos, enemypos, elapsedtime);
            projectile.transform.rotation = Quaternion.Euler(45, 45, (MathHelper.GetAngle(pos, enemypos) + angle));
            if(elapsedtime>=1)
            {
                stop = true;
            }

            yield return null;
        }
        if (targetChar != null)
        {
            targetChar.Status.DamagedLife(Chara.Status.AD, Chara, target,DamageType.Kinetic);
            Chara.Status.ManaGet(10);
            PixelFx hitfx = Chara.FxCall();
            hitfx.gameObject.SetActive(true);
            hitfx.transform.position = enemypos;
        }
        projectile.ShutActive();
        yield return null;

    }


    public IEnumerator IEMove(BaseChar Chara, Node target)
    {

        Node prevnode = Chara.CurrNode;
        Chara.SetRunning(true);
        string aniname = ChangeRunAni(Direction(Chara.CurrNode, target), Chara);
        target.Setbool(true);
        float elapsedtime = 0;
        Vector3 pos = Chara.transform.position;
        bool stop = false;
        while (!stop)
        {
            if (Chara == null)
            {
                target.Setbool(false);
                yield break;
            }

            if (Chara.Status.Life <= 0)
            {
                target.Setbool(false);
                Chara.m_animator.SetBool(aniname, false);
                Chara.SetRunning(false);
                yield break;
            }

            elapsedtime += Time.deltaTime * m_speed;
            Chara.transform.position = Vector3.Lerp(pos, target.transform.position, elapsedtime);
            if(elapsedtime>=1)
            {
                prevnode.Setbool(false);
                stop = true;
            }
            yield return null;
        }

        if (Chara == null)
            yield break;

        if (aniname != null)
            Chara.m_animator.SetBool(aniname, false);

        Chara.SetRunning(false);

        yield return null;

    }



    public static DIR Direction(Node curr, Node target)
    {
        DIR dir = new DIR();
        int x;
        int y;
        if (target == null)
        {
            x = curr.Row;
            y = curr.Col;

        }
        else
        {
            x = curr.Row - target.Row;
            y = curr.Col - target.Col;
        }
        switch (x)
        {
            case 0:
                switch (y)
                {
                    case 0: break;
                    case 1: return DIR.WEST;
                    case -1: return DIR.EAST;
                }
                break;
            case 1:
                switch (y)
                {
                    case 0: return DIR.NORTH;
                    case 1: return DIR.NORTH;
                    case -1: return DIR.EAST;
                }
                break;
            case -1:
                switch (y)
                {
                    case 0: return DIR.SOUTH;
                    case 1: return DIR.WEST;
                    case -1: return DIR.SOUTH;
                }
                break;

        }

        if (x == 0)
        {
            if (y < 0)
            { return DIR.EAST; }
            else if (y > 0)
            { return DIR.WEST; }
        }
        if (x > 0)
        {
            if (y == 0)
            { return DIR.NORTH; }
            else if (y < 0)
            { return DIR.NORTH; }
            else if (y > 0)
            { return DIR.NORTH; }
        }
        if (x < 0)
        {
            if (y == 0)
            { return DIR.SOUTH; }
            else if (y < 0)
            { return DIR.SOUTH; }
            else if (y > 0)
            { return DIR.SOUTH; }
        }


        return dir;
    }

    private static string ChangeRunAni(DIR dir, BaseChar chara)
    {

        if (chara == null)
            return null;

        float oriscale = Mathf.Abs(chara.transform.localScale.x);
        switch (dir)
        {
            case DIR.EAST:
                chara.m_animator.SetBool("Runbool", true);
                chara.transform.localScale = new Vector3(-1*oriscale, 1*oriscale, 1);
                return "Runbool";
            case DIR.WEST:
                chara.m_animator.SetBool("Runbool2", true);
                chara.transform.localScale = new Vector3(1*oriscale, 1*oriscale, 1);
                return "Runbool2";
            case DIR.SOUTH:
                chara.m_animator.SetBool("Runbool", true);
                chara.transform.localScale = new Vector3(1*oriscale, 1*oriscale, 1);
                return "Runbool";
            case DIR.NORTH:
                chara.m_animator.SetBool("Runbool2", true);
                chara.transform.localScale = new Vector3(-1*oriscale, 1*oriscale, 1);
                return "Runbool2";
        }
        chara.m_animator.SetBool("Runbool", false);
        chara.m_animator.SetBool("Runbool2", false);

        return null;
    }

    private static string ChangeAttackAni(DIR dir, BaseChar chara)
    {
        if (chara == null)
            return null;

        float oriscale = Mathf.Abs(chara.transform.localScale.x);

        switch (dir)
        {
            case DIR.EAST:
                chara.m_animator.SetTrigger("Attack");
                chara.transform.localScale = new Vector3(-1*oriscale, 1*oriscale, 1);
                return "Attack";
            case DIR.WEST:
                chara.m_animator.SetTrigger("Attack2");
                chara.transform.localScale = new Vector3(1*oriscale, 1*oriscale, 1);
                return "Attack2";
            case DIR.SOUTH:
                chara.m_animator.SetTrigger("Attack");
                chara.transform.localScale = new Vector3(1*oriscale, 1*oriscale, 1);
                return "Attack";
            case DIR.NORTH:
                chara.m_animator.SetTrigger("Attack2");
                chara.transform.localScale = new Vector3(-1*oriscale, 1*oriscale, 1);
                return "Attack2";
        }

        return null;
    }

    private static string ChangeSkillAni(DIR dir, BaseChar chara)
    {
        if (chara == null)
            return null;

        float oriscale = Mathf.Abs(chara.transform.localScale.x);

        switch (dir)
        {
            case DIR.EAST:
                chara.m_animator.SetTrigger("Casting");
                chara.transform.localScale = new Vector3(-1*oriscale, 1*oriscale, 1);
                return "Casting";
            case DIR.WEST:
                chara.m_animator.SetTrigger("Casting2");
                chara.transform.localScale = new Vector3(1*oriscale, 1*oriscale, 1);
                return "Casting2";
            case DIR.SOUTH:
                chara.m_animator.SetTrigger("Casting");
                chara.transform.localScale = new Vector3(1*oriscale, 1*oriscale, 1);
                return "Casting";
            case DIR.NORTH:
                chara.m_animator.SetTrigger("Casting2");
                chara.transform.localScale = new Vector3(-1*oriscale, 1*oriscale, 1);
                return "Casting2";
        }

        return null;
    }


    public static string ChangeDeadAni(DIR dir, BaseChar chara)
    {

        if (chara == null)
            return null;

        float oriscale = Mathf.Abs(chara.transform.localScale.x);

        switch (dir)
        {
            case DIR.EAST:
                chara.m_animator.SetTrigger("Dead");
                chara.transform.localScale = new Vector3(-1 * oriscale, 1 * oriscale, 1);
                return "Dead";
            case DIR.WEST:
                chara.m_animator.SetTrigger("Dead");
                chara.transform.localScale = new Vector3(1 * oriscale, 1 * oriscale, 1);
                return "Dead2";
            case DIR.SOUTH:
                chara.m_animator.SetTrigger("Dead");
                chara.transform.localScale = new Vector3(1 * oriscale, 1 * oriscale, 1);
                return "Dead";
            case DIR.NORTH:
                chara.m_animator.SetTrigger("Dead");
                chara.transform.localScale = new Vector3(-1*oriscale, 1*oriscale, 1);
                return "Dead2";
        }
        chara.m_animator.SetBool("Dead", false);
        chara.m_animator.SetBool("Dead2", false);

        return null;

    }

    private bool IsExist(BaseChar Chara, Node target)
    {
        if (Chara == null||!Chara.gameObject.activeInHierarchy)
            return false;

        if (target.CurrCHAR == null)
        {
            Chara.SetAttacking(false);
            return false;
        }
        else
        {
            if (target.CurrCHAR.FOE == Chara.FOE)
            {
                Chara.SetAttacking(false);
                return false;
            }
        }
        return Chara.Attacking;
    }

    int m_currdungeonidx = 0;
    int m_floor = 1;
    private IEnumerator IEupdate()
    {

        WaitForSeconds s5 = new WaitForSeconds(0.5f);
        WaitForSeconds s10 = new WaitForSeconds(1);

        HandDrag.Play("off");

        for (int i = 0; i < CharMng.Instance.CurrHeros.Count; i++)
        {
            BaseChar bc = CharMng.Instance.CurrHeros[i];
            bc.SetTecticsNode(bc.CurrNode);
        }

        for (int i = 0; i < (TableMng.Instance.Table(TableType.DungeonTable, m_currdungeonidx) as Dungeon).m_maps.Count; i++)
        {
            DungeonCreator.Instance.LoadFloor(m_currdungeonidx, i);
            CharMng.Instance.CharSummon();
            CanvasMng.Instance.LoadStateBar();
            yield return s10;
            while (!(CharMng.Instance.CurrEnemys.Count == 0 || CharMng.Instance.CurrHeros.Count == 0))
            {

                Routine();
                yield return s5;
            }
            yield return s10;
            if (CharMng.Instance.CurrHeros.Count > 0)
            {
                CharMng.Instance.CharUnSummon();
                CanvasMng.Instance.ALLOffStateBar();
                yield return s5;
                NextStage.Instance.MoveNext();
                yield return s10;
            }


        }
        //next page

        HandDrag.Play("on"); 
        yield return null;
    }


    public void tempbutton1()
    {

        foreach (var x in HandMng.Instance.FieldChars)
        {
            CharMng.Instance.AddHero(x);
        }
        StartCoroutine(IEupdate());
    }

    public void tempbutton2()
    {
        foreach (var x in CharMng.Instance.CurrHeros)
        { x.Status.ManaGet(100); }
    }
}
