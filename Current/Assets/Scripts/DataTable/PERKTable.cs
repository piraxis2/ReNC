using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PERKTable : ReadData 
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
            Perk perk = new Perk();
            for (int i = 0; i < subject.Length; i++)
            {
                switch (subject[i])
                {
                    case "IDX":
                        int.TryParse(val[i], out perk.m_idx);
                        break;
                    case "NAME":
                        perk.m_name = val[i];
                        break;
                    case "TYPE":
                        perk.m_type = val[i];
                        break;
                    case "OPTION":
                        perk.m_option = val[i];
                        break;
                    case "OPTION2":
                        perk.m_option2 = val[i];
                        break;
                }
            }
            AddInfo(perk.m_idx, perk);

        }
    }
}
