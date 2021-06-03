using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionContainer : MonoBehaviour
{
    private static ActionContainer m_attackcontainer;
    public static ActionContainer Instance
    {

        get
        {
            if (m_attackcontainer != null)
                return m_attackcontainer;

            m_attackcontainer = new ActionContainer();
            m_attackcontainer.Init();

            return m_attackcontainer;
        }
    }


    public float m_speed = 1;

    private WaitForSeconds m_BarrageWait = new WaitForSeconds(0.025f);

    private void Init()
    {


    }

    public IEnumerator IEAttack(BaseChar Chara, Node target)
    {

        float AS = Chara.MyStatus.CalculateAttackspeed();
        WaitForSecondsRealtime attackspeed = new WaitForSecondsRealtime(AS);

        float angle = 0;
        while (IsExist(Chara, target))
        {


            if (Chara.MyStatus.Life <= 0)
            {
                Chara.SetAttacking(false);
                yield break;
            }

            if (Chara.MyStatus.m_stuned)
            {
                Chara.SetAttacking(false);
                yield break;
            }

            if (AS != Chara.MyStatus.CalculateAttackspeed())
            {
                AS = Chara.MyStatus.CalculateAttackspeed();
                attackspeed = new WaitForSecondsRealtime(AS);
            }


            ChangeAttackAni(Direction(Chara.CurrNode, target), Chara);
            angle = MathHelper.GetAngle(Chara.transform.position, target.transform.position) + Chara.m_projectileangle;
            PixelFx Hit = Chara.FxCall();
            if (Hit != null)
            {
                Hit.gameObject.SetActive(true);
                Hit.transform.position = target.CurrCHAR.transform.position + new Vector3(0, 0.5f, 0);
            }

            target.CurrCHAR.MyStatus.DamagedLife(Chara.MyStatus.AD, Chara, target, DamageType.Kinetic);

            //onhit
            if (Chara.Skill == Skillname.ManaSteal)
            { target.CurrCHAR.MyStatus.ManaCost(Knight.m_manasteal[Chara.Star]); }



            Chara.MyStatus.ManaGet(5 + Chara.MyStatus.MPS);
            yield return attackspeed;

            if (Chara.MyStatus.Skillon)
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
        float AS = Chara.MyStatus.CalculateAttackspeed();
        WaitForSeconds attackspeed = new WaitForSeconds(AS);


        while (IsExist(Chara, target))
        {
            if (Chara.MyStatus.Life <= 0)
                yield break;

            if (Chara.MyStatus.m_stuned)
                yield break;


            if (AS != Chara.MyStatus.CalculateAttackspeed())
            {
                AS = Chara.MyStatus.CalculateAttackspeed();
                attackspeed = new WaitForSeconds(AS);
            }



            ChangeAttackAni(Direction(Chara.CurrNode, target), Chara);

            Chara.StartCoroutine(IEProjectile(Chara, target));

            yield return attackspeed;
            if (Chara.MyStatus.Skillon)
            {
                Chara.SetAttacking(false);
                yield break;
            }

        }
        Chara.SetAttacking(false);
        yield return null;

    }


    public IEnumerator IERapidShot(BaseChar Chara, Node target)
    {
        float AS = Chara.MyStatus.CalculateAttackspeed();
        WaitForSeconds attackspeed = new WaitForSeconds(AS);

        while (IsExist(Chara, target))
        {
            if (Chara.MyStatus.Life <= 0)
                yield break;

            if (Chara.MyStatus.m_stuned)
                yield break;


            if (AS != Chara.MyStatus.CalculateAttackspeed())
            {
                AS = Chara.MyStatus.CalculateAttackspeed();
                attackspeed = new WaitForSeconds(AS);
            }


            ChangeAttackAni(Direction(Chara.CurrNode, target), Chara);

            Chara.StartCoroutine(IEBarrageProjectile(Chara, target));

            yield return attackspeed;

            if(!Chara.m_rapidshot)
            {
                Chara.SetAttacking(false);
                yield break;
            }


        }
        Chara.SetAttacking(false);
        yield return null;


    }


    public IEnumerator IEWideAttack(BaseChar Chara, Node target)
    {
        float AS = Chara.MyStatus.CalculateAttackspeed();
        WaitForSeconds attackspeed = new WaitForSeconds(AS);


        while (IsExist(Chara, target))
        {
            if (Chara.MyStatus.Life <= 0)
                yield break;

            if (Chara.MyStatus.m_stuned)
                yield break;


            if (AS != Chara.MyStatus.CalculateAttackspeed())
            {
                AS = Chara.MyStatus.CalculateAttackspeed();
                attackspeed = new WaitForSeconds(AS);
            }



            ChangeAttackAni(Direction(Chara.CurrNode, target), Chara);
            List<Node> widetargets = Chara.RangeList;

            for (int i = 0; i < widetargets.Count; i++)
            {
                if (widetargets[i].CurrCHAR != null)
                {
                    if (widetargets[i].CurrCHAR.FOE != Chara.FOE)
                        Chara.StartCoroutine(IEWideProjectile(Chara, widetargets[i]));
                }
                else
                    Chara.StartCoroutine(IEWideProjectile(Chara, widetargets[i]));

            }
            yield return attackspeed;
            if (Chara.MyStatus.Skillon)
            {
                Chara.SetAttacking(false);
                yield break;
            }

        }
        Chara.SetAttacking(false);
        yield return null;

    }


    public IEnumerator IEWideProjectile(BaseChar Chara, Node target)
    {
        if (Chara == null)
            yield break;


        BaseChar targetChar = target.CurrCHAR;
        Vector3 pos = Chara.transform.position + new Vector3(0, 0.5f, 0);

        PixelFx projectile = Chara.ProjectileCall();
        if (projectile != null)
            projectile.gameObject.SetActive(true);

        int angle = Chara.m_projectileangle;

        float elapsedtime = 0;
        bool stop = false;

        while (!stop)
        {
            elapsedtime += Time.deltaTime * 2.5f;
            elapsedtime = Mathf.Clamp01(elapsedtime);


            if (Chara.ProjectileType == Attacktype.Direct)
            {
                projectile.transform.position = Vector3.Lerp(pos, target.transform.position, elapsedtime);
                projectile.transform.rotation = Quaternion.Euler(45, 45, (MathHelper.GetAngle(pos, target.transform.position) + angle));
            }
            else if (Chara.ProjectileType == Attacktype.Howitzer)
                projectile.transform.position = MathHelper.BezierCurve(pos, new Vector3(pos.x, pos.y + 2, pos.z),
                    new Vector3(target.transform.position.x, target.transform.position.y + 2, target.transform.position.z), 
                    target.transform.position, elapsedtime);


            if (elapsedtime >= 1)
            {
                stop = true;
            }

            yield return null;
        }

        Chara.MyStatus.ManaGet(10);
        PixelFx hitfx = Chara.FxCall();
        if (hitfx != null)
        {
            hitfx.gameObject.SetActive(true);
            if (Chara.ProjectileType == Attacktype.Invisible)
                hitfx.transform.position = target.transform.position;
            else
                hitfx.transform.position = target.transform.position;
        }

        if (targetChar != null)
        {
            if (targetChar.FOE != Chara.FOE)
                targetChar.MyStatus.DamagedLife(Chara.MyStatus.AD, Chara, target, DamageType.Kinetic);
        }

        if (projectile != null)
            projectile.ShutActive();
        yield return null;




    }


    public IEnumerator IEProjectile(BaseChar Chara, Node target)
    {
        if (Chara == null)
            yield break;

        BaseChar targetChar = target.CurrCHAR;
        Vector3 pos = Chara.transform.position + new Vector3(0, 0.5f, 0);
        Vector3 enemypos = targetChar.transform.position + new Vector3(0, 0.5f, 0);

        PixelFx projectile = Chara.ProjectileCall();
        if (projectile != null)
            projectile.gameObject.SetActive(true);

        int angle = Chara.m_projectileangle;

        float elapsedtime = 0;
        bool stop = false;

        while (!stop)
        {
            elapsedtime += Time.deltaTime * 8f;
            elapsedtime = Mathf.Clamp01(elapsedtime);


            if (Chara.ProjectileType == Attacktype.Direct)
            {
                projectile.transform.position = Vector3.Lerp(pos, enemypos, elapsedtime);
                projectile.transform.rotation = Quaternion.Euler(45, 45, (MathHelper.GetAngle(pos, enemypos) + angle));
            }
            else if (Chara.ProjectileType == Attacktype.Howitzer)
                projectile.transform.position = MathHelper.BezierCurve(pos, new Vector3(pos.x, pos.y + 2, pos.z), new Vector3(enemypos.x, enemypos.y + 2, enemypos.z), enemypos, elapsedtime);


            if (elapsedtime >= 1)
            {
                stop = true;
            }

            yield return null;
        }
        if (targetChar != null)
        {

            if (Chara.Skill == Skillname.SilverBullet)
            {
                targetChar.MyStatus.DamagedLife(Chara.MyStatus.AD, Chara, target, DamageType.Onhit);
            }
            else
            {
                targetChar.MyStatus.DamagedLife(Chara.MyStatus.AD, Chara, target, DamageType.Kinetic);
            }
            Chara.MyStatus.ManaGet(10);
            PixelFx hitfx = Chara.FxCall();
            if (hitfx != null)
            {
                hitfx.gameObject.SetActive(true);
                if (Chara.ProjectileType == Attacktype.Invisible)
                    hitfx.transform.position = target.transform.position;
                else
                    hitfx.transform.position = enemypos;
            }
        }
        if (projectile != null)
            projectile.ShutActive();
        yield return null;

    }

    public IEnumerator IEBarrageProjectile(BaseChar Chara, Node target)
    {
        if (Chara == null)
            yield break;

        BaseChar targetChar = target.CurrCHAR;
        Vector3 pos = Chara.transform.position + new Vector3(0, 0.5f, 0);
        Vector3 enemypos = targetChar.transform.position + new Vector3(0, 0.5f, 0);

        PixelFx[] projecs = new PixelFx[3];

        for (int i = 0; i < 3; i++)
        {
            projecs[i] = Chara.ProjectileCall();
            if (projecs[i] != null)
                projecs[i].gameObject.SetActive(true);



            int angle = Chara.m_projectileangle;

            float elapsedtime = 0;
            bool stop = false;

            while (!stop)
            {
                elapsedtime += Time.deltaTime * 8f;
                elapsedtime = Mathf.Clamp01(elapsedtime);


                if (Chara.ProjectileType == Attacktype.Direct)
                {
                    projecs[i].transform.position = Vector3.Lerp(pos, enemypos, elapsedtime);
                    projecs[i].transform.rotation = Quaternion.Euler(45, 45, (MathHelper.GetAngle(pos, enemypos) + angle));
                }
                else if (Chara.ProjectileType == Attacktype.Howitzer)
                    projecs[i].transform.position = MathHelper.BezierCurve(pos, new Vector3(pos.x, pos.y + 2, pos.z), new Vector3(enemypos.x, enemypos.y + 2, enemypos.z), enemypos, elapsedtime);


                if (elapsedtime >= 1)
                {
                    stop = true;
                }

                yield return null;
            }
            if (targetChar != null)
            {
                targetChar.MyStatus.DamagedLife(Chara.MyStatus.AD / 2, Chara, target, DamageType.Kinetic);


                PixelFx hitfx = Chara.FxCall();
                if (hitfx != null)
                {
                    hitfx.gameObject.SetActive(true);
                    if (Chara.ProjectileType == Attacktype.Invisible)
                        hitfx.transform.position = target.transform.position;
                    else
                        hitfx.transform.position = enemypos;
                }
            }
            if (projecs[i] != null)
                projecs[i].ShutActive();

            yield return m_BarrageWait;

        }
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

            if (Chara.MyStatus.Life <= 0)
            {
                target.Setbool(false);
                Chara.m_animator.SetBool(aniname, false);
                Chara.SetRunning(false);
                yield break;
            }

            elapsedtime += Time.deltaTime * m_speed;
            Chara.transform.position = Vector3.Lerp(pos, target.transform.position, elapsedtime);
            if (elapsedtime >= 1)
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



    public DIR Direction(Node curr, Node target)
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

    private string ChangeRunAni(DIR dir, BaseChar chara)
    {

        if (chara == null)
            return null;

        float oriscale = Mathf.Abs(chara.transform.localScale.x);
        switch (dir)
        {
            case DIR.EAST:
                chara.m_animator.SetBool("Runbool", true);
                chara.transform.localScale = new Vector3(-1 * oriscale, 1 * oriscale, 1);
                return "Runbool";
            case DIR.WEST:
                chara.m_animator.SetBool("Runbool2", true);
                chara.transform.localScale = new Vector3(1 * oriscale, 1 * oriscale, 1);
                return "Runbool2";
            case DIR.SOUTH:
                chara.m_animator.SetBool("Runbool", true);
                chara.transform.localScale = new Vector3(1 * oriscale, 1 * oriscale, 1);
                return "Runbool";
            case DIR.NORTH:
                chara.m_animator.SetBool("Runbool2", true);
                chara.transform.localScale = new Vector3(-1 * oriscale, 1 * oriscale, 1);
                return "Runbool2";
        }
        chara.m_animator.SetBool("Runbool", false);
        chara.m_animator.SetBool("Runbool2", false);

        return null;
    }

    private string ChangeAttackAni(DIR dir, BaseChar chara)
    {
        if (chara == null)
            return null;

        float oriscale = Mathf.Abs(chara.transform.localScale.x);

        switch (dir)
        {
            case DIR.EAST:
                chara.m_animator.SetTrigger("Attack");
                chara.transform.localScale = new Vector3(-1 * oriscale, 1 * oriscale, 1);
                return "Attack";
            case DIR.WEST:
                chara.m_animator.SetTrigger("Attack2");
                chara.transform.localScale = new Vector3(1 * oriscale, 1 * oriscale, 1);
                return "Attack2";
            case DIR.SOUTH:
                chara.m_animator.SetTrigger("Attack");
                chara.transform.localScale = new Vector3(1 * oriscale, 1 * oriscale, 1);
                return "Attack";
            case DIR.NORTH:
                chara.m_animator.SetTrigger("Attack2");
                chara.transform.localScale = new Vector3(-1 * oriscale, 1 * oriscale, 1);
                return "Attack2";
        }

        return null;
    }

    private string ChangeSkillAni(DIR dir, BaseChar chara)
    {
        if (chara == null)
            return null;

        float oriscale = Mathf.Abs(chara.transform.localScale.x);

        switch (dir)
        {
            case DIR.EAST:
                chara.m_animator.SetTrigger("Casting");
                chara.transform.localScale = new Vector3(-1 * oriscale, 1 * oriscale, 1);
                return "Casting";
            case DIR.WEST:
                chara.m_animator.SetTrigger("Casting2");
                chara.transform.localScale = new Vector3(1 * oriscale, 1 * oriscale, 1);
                return "Casting2";
            case DIR.SOUTH:
                chara.m_animator.SetTrigger("Casting");
                chara.transform.localScale = new Vector3(1 * oriscale, 1 * oriscale, 1);
                return "Casting";
            case DIR.NORTH:
                chara.m_animator.SetTrigger("Casting2");
                chara.transform.localScale = new Vector3(-1 * oriscale, 1 * oriscale, 1);
                return "Casting2";
        }

        return null;
    }


    public string ChangeDeadAni(DIR dir, BaseChar chara)
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
                chara.transform.localScale = new Vector3(-1 * oriscale, 1 * oriscale, 1);
                return "Dead2";
        }
        chara.m_animator.SetBool("Dead", false);
        chara.m_animator.SetBool("Dead2", false);

        return null;

    }

    private bool IsExist(BaseChar Chara, Node target)
    {
        if (Chara == null || !Chara.gameObject.activeInHierarchy)
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
    private bool IsExist(BaseChar Chara, List<Node> targets)
    {
        if (Chara == null || !Chara.gameObject.activeInHierarchy)
            return false;

        int count = 0;
        foreach (var x in targets)
        {
            if (x.CurrCHAR.FOE == Chara.FOE)
                continue;

            count++;
        }

        if (count <= 0)
            Chara.SetAttacking(false);

        return Chara.Attacking;
    }




}
