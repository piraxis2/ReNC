using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    private static Log s_log;
    public static Log Instance
    {
        get
        {
            if (s_log == null)
            {
                s_log = FindObjectOfType<Log>();
                s_log.Init();
            }
            return s_log;
        }
    }

    public Text m_text;
    private void Init()
    {
        m_text = GetComponent<Text>();
    }

    public void CleanText()
    {
        m_text.text = "";
    }

    public void AddText(string text)
    {
        m_text.text += "\n";
        m_text.text += text;
        m_text.text += "\n";
    }

}
