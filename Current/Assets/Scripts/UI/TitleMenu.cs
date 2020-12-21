using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    Button[] m_buttons = new Button[3];


    private void Awake()
    {
        m_buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < 3; i++)
        {
            int x = i;
            m_buttons[i].onClick.AddListener(()=> 
            {
                transform.SendMessage("Menu" + x.ToString(),SendMessageOptions.DontRequireReceiver);
            });
        }

    }

    private void Menu0()
    {

        SceneMng.Instance.Event(Channel.C1, true);
//        LoadingMng.Instance.Fade(false, () => { },1);


    }
    private void Menu1()
    {
    }
    private void Menu2()
    {

    }


}
