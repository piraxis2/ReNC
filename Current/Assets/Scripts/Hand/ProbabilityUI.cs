using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProbabilityUI : MonoBehaviour
{


    private static ProbabilityUI m_probabiltyui;
    public static ProbabilityUI Instance
    {
        get
        {
            if (m_probabiltyui==null)
            {
                m_probabiltyui = FindObjectOfType<ProbabilityUI>();
                m_probabiltyui.Init();
            }


            return m_probabiltyui;
        }
    }

    private Text[] m_numbers;






    private void Init()
    {
        m_numbers = gameObject.GetComponentsInChildren<Text>();
    }


    public void SetNumber()
    {
        for (int i = 0; i < 5; i++)
        {
            m_numbers[i].text = MulliganMng.instance.Rerollprobability[PlayerData.Instance.LV - 1, i].ToString() + '%';
        }

    }

}
