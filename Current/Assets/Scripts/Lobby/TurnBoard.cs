using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBoard : Mng
{

    private Text m_text;
    public float m_speed = 4;


    public override void Init()
    {

        m_text = GetComponentInChildren<Text>();
        Invoke("Drop", 1);
    }

    public void Drop()
    {
        //StartCoroutine(IEdropdown());
    
    }
    public void Up()
    {
        
    }


    public IEnumerator IEdropdown()
    {

        float elapsedtime = 0;
        float rot = 0;
        while(elapsedtime<1)
        {
            elapsedtime += Time.deltaTime * 6f;
            elapsedtime = Mathf.Clamp01(elapsedtime);
            rot = Mathf.Lerp(-90, 10, elapsedtime);
            transform.rotation = Quaternion.Euler(0, 0, rot);

            yield return null;
        }
        elapsedtime = 0;
        while (elapsedtime < 1)
        {
            elapsedtime += Time.deltaTime * 8f;
            elapsedtime = Mathf.Clamp01(elapsedtime);
            rot = Mathf.Lerp(10, -5, elapsedtime);
            transform.rotation = Quaternion.Euler(0, 0, rot);

            yield return null;
        }
        elapsedtime = 0;
        while (elapsedtime < 1)
        {
            elapsedtime += Time.deltaTime * 7f;
            elapsedtime = Mathf.Clamp01(elapsedtime);
            rot = Mathf.Lerp(-5, 0, elapsedtime);
            transform.rotation = Quaternion.Euler(0, 0, rot);

            yield return null;
        }
        elapsedtime = 0;


        yield return null;

    }

}
