using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSize : MonoBehaviour
{




    private RectTransform m_thisrect;
    private RectTransform m_childrect;

    void Awake()
    {
        m_thisrect = GetComponent<RectTransform>();
        m_childrect = transform.Find("View").GetComponent<RectTransform>();
    }


    public void Update()
    {
        if (gameObject.activeInHierarchy)
            m_thisrect.sizeDelta = new Vector2(m_thisrect.sizeDelta.x, m_childrect.sizeDelta.y + 70);
    }

}
