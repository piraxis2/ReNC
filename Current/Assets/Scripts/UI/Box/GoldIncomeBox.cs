using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldIncomeBox : PopupBox
{


    private Text[] m_texts;


    public override void Init()
    {
        m_texts = transform.Find("Gold").GetComponentsInChildren<Text>();


    }
    public override void Setbox(Vector3 pos)
    {

        m_texts[0].text = PlayerData.Instance.IncomeCal("Total").ToString();
        m_texts[1].text = PlayerData.Instance.IncomeCal("Passive").ToString();
        m_texts[2].text = PlayerData.Instance.IncomeCal("Interest").ToString();
        m_texts[3].text = PlayerData.Instance.IncomeCal("Win/Loss").ToString();
        base.Setbox(pos);
    }

}
