using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonCreator : MonoBehaviour
{

    private static DungeonCreator s_dungeoncreator;

    public static DungeonCreator Instance
    {
        get
        {
            if(s_dungeoncreator==null)
            {
                s_dungeoncreator = GameObject.Find("Main/Stage").GetComponent<DungeonCreator>();
                s_dungeoncreator.Init();
            }
            return s_dungeoncreator;
        }
    }

    private NextStage m_stageChanger = new NextStage();
    private NodeMng m_nodeMng = new NodeMng();

    private void Init()
    {
        m_stageChanger = GetComponentInChildren<NextStage>();
        m_nodeMng = GetComponentInChildren<NodeMng>();

    }


    public string debuggg;

    public void LoadFloor(int dungeonidx, int floor)
    {
        List<string[,]> dungeon = (TableMng.Instance.Table(TableType.DungeonTable, dungeonidx) as Dungeon).m_maps;


        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Placement(dungeon[floor][i, j], i, j);
            }
        }
      
    }

    public void Placement(string val, int x, int y)
    {

        if (NodeMng.instance.NodeArr[x, y].IsHere)
            return;

        switch (val[0])
        {
            case 'm':
                BaseChar mob = MobPooling.Instance.CharCall((int)(val[1] - '0'));
                mob.transform.position = NodeMng.instance.NodeArr[x, y].transform.position;
                mob.gameObject.SetActive(true);
                CharMng.Instance.AddEnemy(mob);
                CharMng.Instance.InvisibleCharters(0);
                break;
            case 't':


                break;
            case 's':


                break;
        }


    }






}
