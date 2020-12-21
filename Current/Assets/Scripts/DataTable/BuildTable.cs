using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTable : ReadData
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
            Build passive = new Build();
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
                    case "TEXT":
                        passive.m_info = val[i];
                        break;
                    case "COST":
                        int.TryParse(val[i], out passive.m_cost);
                        break;

                }
            }
            AddInfo(passive.m_idx, passive);

        }
    }

    public System.Action Action(string name)
    {
        switch (name)
        {

        }
        return null;
    }



}
