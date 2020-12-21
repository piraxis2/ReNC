using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopingTable : ReadData
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

        for (int j = 0; j < rowlist.Count; j++)
        {
            string[] val = rowlist[j].Split(',');
            List<int> shoplist = new List<int>();
            for (int i = 0; i < val.Length; i++)
            {
                int x = 0;
                if (int.TryParse(val[i], out x))
                {
                    shoplist.Add(x);
                }
            }
            AddInfo(j, shoplist);
        }
    }
}
