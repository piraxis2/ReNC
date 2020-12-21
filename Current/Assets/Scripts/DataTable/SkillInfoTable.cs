using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillInfoTable : ReadData
{
    protected override void Parse(string text)
    {
        string[] row = text.Split('\n');

        List<string> rowlist = new List<string>();
        for (int i = 0; i < row.Length; ++i)
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
            Skillinfo info = new Skillinfo();
            for (int i = 0; i < subject.Length; ++i)
            {
                switch (subject[i])
                {
                    case "IDX":
                        int.TryParse(val[i], out info.m_idx);
                        break;
                    case "NAME":
                        if (val[i] == "")
                            info.m_name = "none";

                        info.m_name = val[i];
                        break;
                    case "INFO":
                        if (val[i] == "")
                            info.m_info = "none";

                        info.m_info = val[i];
                        break;
                    case "TEXT":
                        if (val[i] == "")
                            info.m_text = "none";
                        info.m_text = val[i];
                        break;
                    case "ICON":
                        info.m_icon = Resources.Load<Sprite>(val[i]);
                        break;

                }
            }
            AddInfo(info.m_idx, info);
        }
    }

}
