using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationData : MonoBehaviour
{

    public List<List<int>> m_shopinglist = new List<List<int>>();

    private static RotationData s_RotationData;

    public static RotationData instance
    {
        get
        {
            if (s_RotationData == null)
            {
                s_RotationData = new RotationData();
                for (int i = 0; i < TableMng.Instance.TableLength(TableType.ShopTable); i++)
                {
                    s_RotationData.m_shopinglist.Add((List<int>)TableMng.Instance.Table(TableType.ShopTable, i));
                }

            }
            return s_RotationData;
        }
    }

}
