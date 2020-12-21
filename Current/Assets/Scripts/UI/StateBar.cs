using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBar : MonoBehaviour
{
    private BaseChar m_char;
    private Image[] m_image = new Image[4];
    private int m_ID = -1;

    public void Charset(BaseChar chara , int onfieldindexer)
    {
        m_ID = onfieldindexer;
        m_char = chara;
        m_image = GetComponentsInChildren<Image>();

        StartCoroutine(IETrackingChar());
    }

    public void CharOut()
    {
        m_image[1].fillAmount = 1;
        m_image[3].fillAmount = 0;
        transform.localPosition = new Vector3(0, 0, 0);
        m_ID = -1;
        m_char = null;
        gameObject.SetActive(false);
    }


    IEnumerator IETrackingChar()
    {
        while (m_char.Status.Life > 0)

        {
            if (m_char == null)
               yield break;

            if (m_char.Running)
                transform.position = Camera.main.WorldToScreenPoint(m_char.transform.position);
            if (m_image[1] != null)
                m_image[1].fillAmount = (float)m_char.Status.Life / m_char.Status.MaxLife;
            if (m_image[3] != null)
                m_image[3].fillAmount = (float)m_char.Status.Mana / m_char.Status.MaxLife;

            yield return null;
            
        }
        
        m_image[1].fillAmount = 1;
        m_image[3].fillAmount = 0;
        transform.localPosition = new Vector3(0, 0, 0);
        m_ID = -1;
        m_char = null;
        gameObject.SetActive(false);
        yield return null;

    }

    

    //private void Update()
    //{
    //    if (m_char == null)
    //    {
    //        gameObject.SetActive(false);
    //        m_image[1].fillAmount = 1;
    //        m_image[3].fillAmount = 0;
    //        transform.localPosition = new Vector3(0, 0, 0);
    //        m_ID = -1;
    //    }
    //    else
    //    {
    //        if (m_char.Running)
    //        {
    //            transform.position = Camera.main.WorldToScreenPoint(m_char.transform.position);
    //        }
    //    }
    //    if (m_image[1] != null)
    //        m_image[1].fillAmount =(float)m_char.Status.Life / 100;
    //    if (m_image[3] != null)
    //        m_image[3].fillAmount = (float)m_char.Status.Mana / 100;
        
    //}

  
}
