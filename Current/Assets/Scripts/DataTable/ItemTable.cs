using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemTable : ReadData
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
            Item item = new Item();
            for (int i = 0; i < subject.Length; ++i)
            {
                switch (subject[i])
                {
                    case "IDX":
                        int.TryParse(val[i], out item.m_idx);
                        break;
                    case "NAME":
                        item.m_name = val[i];
                        break;
                    case "TYPE":
                        item.m_type = val[i];
                        break;
                    case "AD":
                        item.m_ad = val[i];
                        break;
                    case "AP":
                        item.m_ap = val[i];
                        break;
                    case "AS":
                        item.m_as = val[i];
                        break;
                    case "DF":
                        item.m_df = val[i];
                        break;
                    case "MANA":
                        item.m_mana = val[i];
                        break;
                    case "LIFE":
                        item.m_life = val[i];
                        break;
                    case "LIFERECOVERY":
                        item.m_liferecovery = val[i]; 
                        break;
                    case "MANARECOVERY":
                        item.m_manarecovery = val[i]; 
                        break;
                    case "CLI":
                        item.m_cli = val[i];
                        break;
                    case "QUALITY":
                        item.m_quality = val[i];
                        break;
                    case "PRICE":
                        int.TryParse(val[i], out item.m_price);
                        break;
                    case "SPRITE":
                        item.m_sprite = Resources.Load<Sprite>(val[i]);
                        break;
                    case "LORE":
                        item.m_loer = val[i];
                        break;
                    case "AVOID":
                        item.m_avoid = val[i];
                        break;

                }
            }
            AddInfo(item.m_idx, item);
        }
    }

    public static float ParseType(string type,int oristat)
    {
        switch(type[0])
        {
            case 'p': return oristat + float.Parse(type);
            case 'm': return oristat * float.Parse(type);
            case 'd': return oristat - float.Parse(type);
        }
        return oristat;
    }
}
