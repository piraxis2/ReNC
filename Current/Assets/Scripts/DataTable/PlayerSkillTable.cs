using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillTable : ReadData
{
    protected override void Parse(string text)
    {
        string[] row = text.Split('\n');

        List<string> rowlist = new List<string>();
        for (int i = 0; i < row.Length; i++)
        {
            if (!string.IsNullOrEmpty(row[i]))
            {
                string s = row[i].Replace('\r', ' ');
                rowlist.Add(s.Trim());
            }
        }

        string[] subject = rowlist[0].Split(',');

        for (int j = 1; j < rowlist.Count; j++)
        {
            string[] val = rowlist[j].Split(',');
            PlayerSkill skill = new PlayerSkill();
            for (int i = 0; i < subject.Length; i++)
            {
                switch (subject[i])
                {
                    case "IDX":
                        int.TryParse(val[i], out skill.m_idx);
                        skill.m_option = Action(skill.m_idx);
                        break;
                    case "NAME":
                        skill.m_name = val[i];
                        break;
                    case "INFO":
                        skill.m_info = val[i];
                        break;
                    case "COST":
                        int.TryParse(val[i], out skill.m_cost);
                        break;
                    case "ICON":
                        skill.m_icon = Resources.Load<Sprite>(val[i]);
                        break;
                }
            }
            AddInfo(skill.m_idx, skill);

        }
    }

    public System.Action Action(int idx)
    {

        switch (idx)
        {
            case 0: return Heal;
            case 1: return Enhance;
            case 2: return Camping;
            case 3: return FireSupport;
            case 4: return Sniping;
            case 5: return Flamestrike;
            case 6: return Run;
            case 7: return Greed;
            case 8: return Hearthstone;
        }

        return null;
    }

    public void Heal()
    {

        if (CharMng.Instance == null)
            return;

        for (int i = 0; i < CharMng.Instance.CurrHeros.Count; i++)
        {
            CharMng.Instance.CurrHeros[i].MyStatus.CuredLife(30);
            PixelFx fx = FxMng.Instance.FxCall("Heal");
            fx.gameObject.SetActive(true);
            fx.transform.position = CharMng.Instance.CurrHeros[i].transform.position;
        }
    }

    private void Enhance()
    {
        if (CharMng.Instance == null)
            return;

        for(int i = 0; i<CharMng.Instance.CurrHeros.Count; i++)
        {
            CharMng.Instance.CurrHeros[i].MyStatus.GetBuff("Enhance", 8f);
            PixelFx fx = FxMng.Instance.FxCall("Buff");
            fx.gameObject.SetActive(true);
            fx.transform.position = CharMng.Instance.CurrHeros[i].transform.position;
        }

    }

    private void Camping()
    {
        if (CharMng.Instance == null)
            return;
    }

    private void FireSupport()
    {
        if (CharMng.Instance == null)
            return;

        for (int i = 0; i < CharMng.Instance.CurrEnemys.Count; i++)
        {
            BaseChar target = CharMng.Instance.CurrEnemys[i];
            PixelFx fx = FxMng.Instance.FxCall("Rain5");
            fx.gameObject.SetActive(true);
            fx.transform.position = CharMng.Instance.CurrEnemys[i].transform.position;
            target.MyStatus.DamagedLife(15, null, target.CurrNode, DamageType.Kinetic);
        }
    }

    private void Sniping()
    {
        if (CharMng.Instance == null)
            return;



        int count = CharMng.Instance.CurrEnemys.Count-1;
        int num = Random.Range(0, count);
        BaseChar target = null;
        if (CharMng.Instance.CurrEnemys.Count != 0)
        { target = CharMng.Instance.CurrEnemys[num]; }
        if (target == null)
            return;

        CharMng.Instance.StartCoroutine(IEProjectile(target.CurrNode));


    }

    private void Flamestrike()
    {
        int count = CharMng.Instance.CurrEnemys.Count - 1;
        int num = Random.Range(0, count);
        Node target = CharMng.Instance.CurrEnemys[num].CurrNode;
        List<Node> skillrange = new List<Node>();
        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                if (!(target.Row + row > 7 || target.Row + row < 0 || target.Col + col > 7 || target.Col + col < 0))
                {
                    skillrange.Add(NodeMng.instance.NodeArr[target.Row + row, target.Col + col]);
                }
                else
                {
                    skillrange.Add(null);
                }
            }
        }
        int index2 = 0;

        for (int i = 0; i < 3; i++)
        {
            index2 = i - 3;
            for (int j = 0; j < 3; j++)
            {
                index2 += 3;

                if (skillrange[index2] == null)
                    continue;

                if (skillrange[index2].CurrCHAR != null)
                {
                    if (skillrange[index2].CurrCHAR.FOE)
                        continue;
                }

                PixelFx thunder = FxMng.Instance.FxCall("FireStrike");
                thunder.gameObject.SetActive(true);
                thunder.transform.position = skillrange[index2].transform.position;

                if (skillrange[index2].CurrCHAR == null)
                    continue;


                if (!skillrange[index2].CurrCHAR.FOE)
                    skillrange[index2].CurrCHAR.MyStatus.DamagedLife(50, null, skillrange[index2], DamageType.Skill);


            }

        }


    }

    private void Run()
    {

    }

    private void Greed()
    {

    }

    private void Hearthstone()
    {

    }

    public IEnumerator IEProjectile(Node target)
    {
        if (target.CurrCHAR == null)
            yield break;

        BaseChar targetChar = target.CurrCHAR;
        Vector3 pos = new Vector3(-10, 0, 0);
        Vector3 enemypos = targetChar.transform.position + new Vector3(0, 1f, 0);

        PixelFx projectile = FxMng.Instance.FxCall("Sniping");
        projectile.gameObject.SetActive(true);


        float elapsedtime = 0;
        bool stop = false;

        while (!stop)
        {
            elapsedtime += Time.deltaTime * 3;
            elapsedtime = Mathf.Clamp01(elapsedtime);


            projectile.transform.position = Vector3.Lerp(pos, enemypos, elapsedtime);
            projectile.transform.rotation = Quaternion.Euler(45, 45, (MathHelper.GetAngle(pos, enemypos)));
            if (elapsedtime >= 1)
            {
                stop = true;
            }

            yield return null;
        }
        stop = false;
        elapsedtime = 0;

        if (targetChar != null)
        {
            targetChar.MyStatus.DamagedLife(50, null, targetChar.CurrNode, DamageType.Kinetic, "HeadShot!");
            PixelFx hitfx = FxMng.Instance.FxCall("Boom");
            hitfx.gameObject.SetActive(true);
            hitfx.transform.position = enemypos;
        }
        projectile.ShutActive();
        yield return null;

    }
}
