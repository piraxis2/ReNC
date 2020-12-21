using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTable : ReadData
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
        string[,] tiles = new string[4, 8];
        Dungeon dungeon = new Dungeon();
        for (int i = 0; i < rowlist.Count; i++)
        {
            
            string[] val = rowlist[i].Split(',');
            if (i % 5 == 0)
            {
                if (val[0] == "0floor")
                {
                    if (i != 0)
                    {
                        AddInfo(dungeon.m_idx, dungeon);
                    }
                    tiles = new string[4, 8];
                    dungeon = new Dungeon();
                    int.TryParse(val[1], out dungeon.m_idx);
                    dungeon.m_name = val[2];
                    dungeon.m_path = val[3];
                }
                else
                {
                    dungeon.m_maps.Add(tiles);
                    tiles = new string[4, 8];
                    continue;
                }
               
            }
            else
            {
                for (int j = 0; j < 8; j++)
                {
                    tiles[(i % 5)-1, j] = val[j];
                }
            }
        }

        AddInfo(dungeon.m_idx, dungeon);

    }


}
