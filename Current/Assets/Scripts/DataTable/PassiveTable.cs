using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveTable : ReadData
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
            Passive passive = new Passive();
            for (int i = 0; i < subject.Length; i++)
            {
                switch (subject[i])
                {
                    case "IDX":
                        int.TryParse(val[i], out passive.m_idx);
                        break;
                    case "NAME":
                        passive.m_name = val[i];
                        passive.m_option = Action(val[i]);
                        break;
                    case "OPTION":
                        passive.m_optiontxt = val[i];
                        
                        break;
                }
            }
            AddInfo(passive.m_idx, passive);

        }
    }

    public System.Action<Status> Action(string name)
    {
        switch(name)
        {
            case "날쌘돌이": return Sonic;
            case "돌대가리": return Stonehead;
            case "튼튼이": return Robust;
            case "초원의 눈": return Glasslandeyes;
            case "강인함": return Strong;
            case "날선 감각": return SharpMind;
            case "마법내성": return MagicRegest;
            case "죽음의 일격": return DeathStrike;
            case "불사": return Undying;
            case "대마법사": return Arcmage;
            case "대장군": return Warlord;
            case "천재": return Genius;
        }
        return null;
    }
    void Sonic(Status status)
    {
        status.m_equavoid += 2;
    }

    void Stonehead(Status status)
    {
        status.m_equad += 10;
        status.m_equap -= 10;
    }

    void Robust(Status status)
    {
        status.m_equdf += 5;
    }
    void Glasslandeyes(Status status)
    {
        status.m_equran += 1;
    }
    void Strong(Status status)
    {
        status.m_equlife = (int)(status.m_equlife * 1.1f);
    }
    void SharpMind(Status status)
    {
        status.m_equcli += 2;
    }

    void MagicRegest(Status status)
    {
        //done
    }

    void DeathStrike(Status status)
    {
        //done
    }

    void Undying(Status status)
    {
        //done
    }

    void Arcmage(Status status)
    {
        //done
    }

    void Warlord(Status status)
    {
        status.m_equad *= 2;
    }

    void Genius(Status status)
    {

    }
}
