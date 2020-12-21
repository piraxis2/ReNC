using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningTile : MonoBehaviour
{
    private SpriteRenderer m_renderer;

    public bool m_Isrunning = false;

    private void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    public SpriteRenderer GetSpriteRenderer
    {
        get { return m_renderer; }
    }

    public void StartWarning()
    {
        StartCoroutine(IEblink());   
    }

    public IEnumerator IEblink()
    {
        m_Isrunning = true;
        Color origin = m_renderer.color;
        Color target = m_renderer.color - new Color(0, 0, 0, m_renderer.color.a);
        float elapsedtime = 0;

        for (int i = 0; i < 4; i++)
        {
            bool stop = false;
            while (!stop)
            {
                elapsedtime += (Time.deltaTime * 4);
                elapsedtime = Mathf.Clamp01(elapsedtime);
                m_renderer.color = Color.Lerp(target, origin, elapsedtime);
                if (elapsedtime >= 1)
                {
                    elapsedtime = 0;
                    stop = true;
                }
                yield return null;
            }
            Color temp;
            temp = origin;
            origin = target;
            target = temp;
        }
        m_renderer.color += new Color(0, 0, 0, 1);
        m_Isrunning = false;
        gameObject.SetActive(false);
              
        yield return null;
    }

}
