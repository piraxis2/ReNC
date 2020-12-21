using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clips : MonoBehaviour
{

    Button[] m_buttons = new Button[3];

    public void Init()
    {
        m_buttons[0] = transform.Find("Status").GetComponent<Button>();
        m_buttons[1] = transform.Find("Perk").GetComponent<Button>();
        m_buttons[2] = transform.Find("Equip").GetComponent<Button>();

        for(int i = 0;i<3;i++)
        {
            int idx = i;
            m_buttons[i].onClick.AddListener(() => { Sibilding(idx); });
        }
        Sibilding(0);

    }

    private void Sibilding(int idx)
    {
        m_buttons[idx].transform.SetAsLastSibling();
    }

}
