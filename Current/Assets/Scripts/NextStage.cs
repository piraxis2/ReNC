using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    Vector3[] m_oripos = new Vector3[2];
    Transform m_nextstage;
    Transform m_prevstage;
    public float m_speed = 2;
    GameObject Stage;


    private static NextStage s_stage;

    public static NextStage Instance
    {
        get
        {
            if(s_stage == null)
            {
                s_stage = FindObjectOfType<NextStage>();
                s_stage.Init();
            }
            return s_stage;
        }
    }


    public void Init()
    {
        m_oripos[0] = new Vector3(30, 0f, 0);
        m_oripos[1] = new Vector3(0,-30, 0);
        m_nextstage = transform.GetChild(0);
        m_prevstage = transform.GetChild(1);
        Stage = GameObject.Find("Main/Stage/NodeMng");

    }


    IEnumerator IEMoveStage()
    {

        m_nextstage.position = m_oripos[0];
        m_prevstage.position = Vector3.zero;
        float elapsedtime = 0;
        bool stop = false;
        while(!stop)
        {
            Stage.SetActive(false);
            elapsedtime += Time.deltaTime * m_speed;
            m_nextstage.position = Vector3.Lerp(m_oripos[0], Vector3.zero, elapsedtime);
            m_prevstage.position = Vector3.Lerp(Vector3.zero, m_oripos[1], elapsedtime);

            if (elapsedtime >= 1)
            {
                elapsedtime = 0;
                Stage.SetActive(true);
                stop = true;
            }

            yield return null;

        }
       

        //while(elapsedtime<1)
        //{
        //    elapsedtime += Time.deltaTime ;
        //    m_prevstage.position = Vector3.Lerp(m_oripos[1], new Vector3(-10, -10, 0), elapsedtime);
        //    yield return null;

        //}
     

        yield return null;

    }

    public void MoveNext()
    {
        StartCoroutine(IEMoveStage());
    }

}
